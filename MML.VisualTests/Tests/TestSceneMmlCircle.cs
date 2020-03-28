using NUnit.Framework;

namespace MML.VisualTests.Tests
{
    public class TestSceneMmlCircle : MmlTestScene
    {
        private const string TestData = "<mml>" +
                                         "    <Circle width=\"250\" height=\"250\" colour=\"#FF0000\"             position=\"0,0\"/>" +
                                         "    <Circle width=\"250\" height=\"250\" colour=\"green\"               position=\"100,100\"/>" +
                                         "    <Circle width=\"250\" height=\"250\" colour=\"rgba(0, 0, 255, .4)\" position=\"200,200\"/>" +
                                         "</mml>";

        [Test]
        public void TestCircle()
        {
            AddStep("Parse MML", PerformParse);
        }

        private void PerformParse()
        {
            var parser = new MmlParser(TestData);
            var display = new MmlDisplayContainer(parser);

            Child = display;
        }
    }
}
