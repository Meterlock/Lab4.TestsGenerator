using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGeneratorLibrary
{
    class TestsGenerator
    {
        public TestsGenerator(Config _config)
        {
            config = _config;
        }

        private Config config;

        public void Generate(List<string> inputFiles, string outputPath)
        {
        }
    }
}
