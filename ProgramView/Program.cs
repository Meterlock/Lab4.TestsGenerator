using System;
using System.Collections.Generic;
using TestsGeneratorLibrary;

namespace ProgramView
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceFiles = new List<string>();
            sourceFiles.Add(@"D:\УНИВЕР\5 семестр\СПП\Test\input\Tracer.cs");
            sourceFiles.Add(@"D:\УНИВЕР\5 семестр\СПП\Test\input\Writer.cs");
            string outputPath = @"D:\УНИВЕР\5 семестр\СПП\Test\output";

            int maxReadFiles = 2;
            int maxProcessingTasks = 3;
            int maxWriteFiles = 4;
            var cfg = new Config(maxReadFiles, maxProcessingTasks, maxWriteFiles);

            var generator = new TestsGenerator(cfg);
            generator.Generate(sourceFiles, outputPath).Wait();

            Console.WriteLine("Generation ended");
            Console.ReadKey();
        }
    }
}
