using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestsGeneratorLibrary
{
    class AsyncReader
    {
        public async Task<string> Read(string path)
        {
            using (StreamReader strmReader = new StreamReader(path))
            {
                return await strmReader.ReadToEndAsync();
            }
        }
    }
}
