using System.Threading.Tasks;
using System.IO;

namespace TestsGeneratorLibrary
{
    public static class AsyncReader
    {
        public static async Task<string> Read(string filePath)
        {
            using (StreamReader strmReader = new StreamReader(filePath))
            {
                return await strmReader.ReadToEndAsync();
            }
        }
    }
}
