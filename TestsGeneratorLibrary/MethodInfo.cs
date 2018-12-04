using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGeneratorLibrary
{
    class MethodInfo
    {
        public MethodInfo(string methodName)
        {
            MethodName = methodName;
        }

        public string MethodName { get; set; }
    }
}
