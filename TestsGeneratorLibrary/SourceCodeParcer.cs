using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGeneratorLibrary
{
    class SourceCodeParcer
    {
        public ParcingInfo Parce(string sourceCode)
        {
            return new ParcingInfo(new List<ClassInfo>());
        }

        private List<ClassInfo> GetClasses()
        {
            return new List<ClassInfo>();
        }

        private List<MethodInfo> GetMethods()
        {
            return new List<MethodInfo>();
        }
    }
}
