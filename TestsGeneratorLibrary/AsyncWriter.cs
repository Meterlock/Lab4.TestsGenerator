using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace TestsGeneratorLibrary
{
    public static class AsyncWriter
    {
        public static async Task Write(string outputPath, List<TestInfo> tests)
        {
            string path;
            if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);
            foreach (TestInfo test in tests)
            {
                path = outputPath + "\\" + test.TestName;
                using (StreamWriter writer = new StreamWriter(path))
                {
                    await writer.WriteAsync(test.TestContent);
                }
            }
        }
    }
}
