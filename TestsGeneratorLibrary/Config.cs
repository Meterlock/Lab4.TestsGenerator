using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGeneratorLibrary
{
    class Config
    {
        public Config(int maxReadFiles, int maxProcessingTasks, int maxWriteFiles)
        {
            MaxReadFiles = maxReadFiles;
            MaxProcessingTasks = maxProcessingTasks;
            MaxWriteFiles = maxWriteFiles;
        }

        public int MaxReadFiles { get; set; }
        public int MaxProcessingTasks { get; set; }
        public int MaxWriteFiles { get; set; }        
    }
}
