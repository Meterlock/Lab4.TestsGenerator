using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGeneratorLibrary
{
    class TestInfo
    {
        public TestInfo(string testName, string testContent)
        {
            TestName = testName;
            TestContent = testContent;
        }

        public string TestName { get; set; }
        public string TestContent { get; set; }
    }
}
