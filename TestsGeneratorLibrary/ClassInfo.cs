using System.Collections.Generic;

namespace TestsGeneratorLibrary
{
    public class ClassInfo
    {
        public ClassInfo(string className, string classNamespace, List<string> classMethods)
        {
            ClassName = className;
            ClassNamespace = classNamespace;
            ClassMethods = classMethods;
        }

        public string ClassName { get; set; }
        public string ClassNamespace { get; set; }
        public List<string> ClassMethods { get; set; }
    }
}
