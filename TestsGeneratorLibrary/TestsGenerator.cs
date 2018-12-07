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

            var readBlock = new TransformBlock<string, string>(fileName => AsyncReader.Read(fileName), readOptions);
            var processBlock = new TransformBlock<string, List<TestInfo>>(sourceCode => GenerateTests(sourceCode), processOptions);
            var writeBlock = new ActionBlock<List<TestInfo>>(output => AsyncWriter.Write(outputPath, output).Wait(), writeOptions);

            readBlock.LinkTo(processBlock, linkOptions);
            processBlock.LinkTo(writeBlock, linkOptions);
            foreach (string file in inputFiles)
            {
                readBlock.SendAsync(file);
            }
            readBlock.Complete();
            return writeBlock.Completion;
        }

        public List<TestInfo> GenerateTests(string sourceCode)
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