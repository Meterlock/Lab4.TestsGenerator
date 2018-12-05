using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGeneratorLibrary
{
    public class ParcingInfo
    {
        public ParcingInfo(List<ClassInfo> classes)
        {
            Classes = classes;
        }

        public List<ClassInfo> Classes { get; }
    }
}
