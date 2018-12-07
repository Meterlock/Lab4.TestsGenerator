namespace TestsGeneratorLibrary
{
    public class TestInfo
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
