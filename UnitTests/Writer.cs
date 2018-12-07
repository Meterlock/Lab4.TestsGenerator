using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class ConsoleWriter : IWriter
    {
        public void Write(ISerializer serializer, TraceResult traceResult)
        {
            using (Stream console = Console.OpenStandardOutput())
            {
                serializer.Serialize(console, traceResult);
            }
        }
    }


    public class FileWriter : IWriter
    {
        private string _filename;

        public FileWriter(string filename)
        {
            _filename = filename;
        }

        public void Write(ISerializer serializer, TraceResult traceResult)
        {
            using (var fs = new FileStream(_filename, FileMode.Create))
            {
                serializer.Serialize(fs, traceResult);
            }
        }
    }
}
