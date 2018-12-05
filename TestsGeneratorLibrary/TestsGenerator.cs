using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void Generate(List<string> inputFiles, string outputPath)
        {
            DataflowLinkOptions linkOptions = new DataflowLinkOptions();
            linkOptions.PropagateCompletion = true;
            ExecutionDataflowBlockOptions readOptions = new ExecutionDataflowBlockOptions();
            readOptions.MaxDegreeOfParallelism = config.MaxReadFiles;
            ExecutionDataflowBlockOptions processOptions = new ExecutionDataflowBlockOptions();
            processOptions.MaxDegreeOfParallelism = config.MaxProcessingTasks;
            ExecutionDataflowBlockOptions writeOptions = new ExecutionDataflowBlockOptions();
            writeOptions.MaxDegreeOfParallelism = config.MaxWriteFiles;
        }

        private List<TestInfo> GenerateTests(string sourceCode)
        {
            SourceCodeParcer parcer = new SourceCodeParcer();
            ParcingInfo res = parcer.Parce(sourceCode);
            // tests generation
            return new List<TestInfo>();
        }
    }
}
