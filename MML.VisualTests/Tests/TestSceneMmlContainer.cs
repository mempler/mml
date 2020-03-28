using NUnit.Framework;

namespace MML.VisualTests.Tests
{
    public class TestSceneMmlContainer : MmlTestScene
    {
        private const string TestData1 = ""
                                           + "<mml>"
                                           + "    <container width=\"250\" height=\"250\">"
                                           + "        <box width=\"250\" height=\"250\" colour=\"orange\" />"
                                           + "        <sprite texture=\"https://a.ppy.sh/10291354\" width=\"200\" height=\"200\" margin=\"25\" />"
                                           + "    </container>"
                                           + "</mml>";

        [Test]
        public void TestNestedContainers()
        {
            AddStep("Parse MML", PerformParse);
        }

        private void PerformParse()
        {
            var parser = new MmlParser(TestData1);
            var display = new MmlDisplayContainer(parser);

            Child = display;
        }
    }
}
