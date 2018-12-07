using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TestsGeneratorLibrary
{
    public class TestsGenerator
    {
        public TestsGenerator(Config _config)
        {
            config = _config;
        }

        private Config config;

        public Task Generate(List<string> inputFiles, string outputPath)
        {
            var linkOptions = new DataflowLinkOptions();
            linkOptions.PropagateCompletion = true;
            var readOptions = new ExecutionDataflowBlockOptions();
            readOptions.MaxDegreeOfParallelism = config.MaxReadFiles;
            var processOptions = new ExecutionDataflowBlockOptions();
            processOptions.MaxDegreeOfParallelism = config.MaxProcessingTasks;
            var writeOptions = new ExecutionDataflowBlockOptions();
            writeOptions.MaxDegreeOfParallelism = config.MaxWriteFiles;

            TransformBlock<string, string> readBlock = new TransformBlock<string, string>(new Func<string, 
                Task<string>>(AsyncReader.Read), readOptions);
            TransformBlock<string, List<TestInfo>> processBlock = new TransformBlock<string, List<TestInfo>>
                (new Func<string, List<TestInfo>>(GenerateTests), processOptions);
            ActionBlock<List<TestInfo>> writeBlock = new ActionBlock<List<TestInfo>>
                ((output => AsyncWriter.Write(outputPath, output).Wait()), writeOptions);

            readBlock.LinkTo(processBlock, linkOptions);
            processBlock.LinkTo(writeBlock, linkOptions);
            foreach (string file in inputFiles)
            {
                readBlock.SendAsync(file);
            }
            readBlock.Complete();
            return writeBlock.Completion;
        }

        private List<TestInfo> GenerateTests(string sourceCode)
        {
            var parcer = new SourceCodeParcer();
            List<ClassInfo> res = parcer.Parce(sourceCode);
            // tests generation
            var tmplGenerator = new TemplateGenerator();
            List<TestInfo> tests = tmplGenerator.MakeTemplates(res);
            return tests;
        }
    }
}