﻿namespace TestsGeneratorLibrary
{
    public class Config
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
