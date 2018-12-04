using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGeneratorLibrary
{
    class ClassInfo
    {
        public ClassInfo(string className, string classNamespace, List<MethodInfo> classMethods)
        {
            ClassName = className;
            ClassNamespace = classNamespace;
            ClassMethods = classMethods;
        }

        public string ClassName { get; set; }
        public string ClassNamespace { get; set; }
        public List<MethodInfo> ClassMethods { get; set; }
    }
}
