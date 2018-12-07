using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using TestsGeneratorLibrary;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private List<ClassInfo> parcedClasses;
        private List<TestInfo> generatedTests;

        [TestInitialize]
        public void Initialize()
        {
            string path = @"D:\УНИВЕР\5 семестр\СПП\Test\input\Writer.cs";

            int maxReadFiles = 2;
            int maxProcessingTasks = 3;
            int maxWriteFiles = 4;
            var cfg = new Config(maxReadFiles, maxProcessingTasks, maxWriteFiles);
            var generator = new TestsGenerator(cfg);

            string sourceCode;
            using (StreamReader strmReader = new StreamReader(path))
            {
                sourceCode = strmReader.ReadToEnd();
            }

            var parcer = new SourceCodeParcer();
            parcedClasses = parcer.Parce(sourceCode);
            generatedTests = generator.GenerateTests(sourceCode);
        }

        [TestMethod]
        public void ClassAmountTest()
        {
            Assert.AreEqual(parcedClasses.Count, 2);
        }

        [TestMethod]
        public void ClassEqualNamespacesTest()
        {
            Assert.AreEqual(parcedClasses[0].ClassNamespace, parcedClasses[1].ClassNamespace);
        }

        [TestMethod]
        public void ClassInfoTest()
        {
            Assert.AreEqual(parcedClasses[0].ClassNamespace, "Tracer");
            Assert.AreEqual(parcedClasses[1].ClassNamespace, "Tracer");
            Assert.AreEqual(parcedClasses[0].ClassName, "ConsoleWriter");
            Assert.AreEqual(parcedClasses[1].ClassName, "FileWriter");
            Assert.AreEqual(parcedClasses[0].ClassMethods[0], "Write");
            Assert.AreEqual(parcedClasses[1].ClassMethods[0], "Write");
        }

        [TestMethod]
        public void TestClassesAmountTest()
        {
            Assert.AreEqual(generatedTests.Count, 2);
        }

        [TestMethod]
        public void TestNameTest()
        {
            Assert.AreEqual(generatedTests[0].TestName, "ConsoleWriterTest.cs");
            Assert.AreEqual(generatedTests[1].TestName, "FileWriterTest.cs");
        }

        [TestMethod]
        public void TestContentTest()
        {
            generatedTests[0].TestContent.Contains("Tracer.Tests");
            generatedTests[1].TestContent.Contains("Tracer.Tests");
            generatedTests[0].TestContent.Contains("ConsoleWriterTests");
            generatedTests[1].TestContent.Contains("FileWriterTests");
            generatedTests[0].TestContent.Contains("WriteTest");
            generatedTests[1].TestContent.Contains("WriteTest");
        }
    }
}
