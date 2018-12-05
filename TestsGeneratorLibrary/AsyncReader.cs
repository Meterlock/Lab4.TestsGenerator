﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestsGeneratorLibrary
{
    public static class AsyncReader
    {
        public static async Task<string> Read(string path)
        {
            using (StreamReader strmReader = new StreamReader(path))
            {
                return await strmReader.ReadToEndAsync();
            }
        }
    }
}
