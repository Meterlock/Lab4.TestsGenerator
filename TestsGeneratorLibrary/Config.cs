using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGeneratorLibrary
{
    class Config
    {
        public Config(int _MaxReadFiles, int _MaxProcessingTasks, int _MaxWriteFiles)
        {
            MaxReadFiles = _MaxReadFiles;
            MaxProcessingTasks = _MaxProcessingTasks;
            MaxWriteFiles = _MaxWriteFiles;
        }

        public int MaxReadFiles { get; set; }
        public int MaxProcessingTasks { get; set; }
        public int MaxWriteFiles { get; set; }        
    }
}
