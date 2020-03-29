using NUnit.Framework;

namespace MML.VisualTests.Tests
{
    public class TestSceneMmlBox : MmlTestScene
    {
        private const string TestData = "<mml>" +
                                         "    <Box width=\"250px\" height=\"250px\" colour=\"#FFFF00\" position=\"-50,400\"/>" +
                                         "    <Box width=\"250px\" height=\"250px\" colour=\"#FF0000\" position=\"0,0\"/>" +
                                         "    <Box width=\"2.6041in\" height=\"2.6041in\" colour=\"rgba(0, 0, 255, .4)\" position=\"100,100\"/>" +
                                         "    <Box width=\"187.5pt\" height=\"187.5pt\" colour=\"rgba(0, 255, 255, .6)\" position=\"200,200\"/>" +
                                         "    <Box width=\"15.625pc\" height=\"15.625pc\" colour=\"rgba(255, 0, 255, .8)\" position=\"300,300\"/>" +
                                         "</mml>";

        [Test]
        public void TestBox()
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
