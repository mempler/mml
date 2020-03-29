using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;

namespace MML.VisualTests.Tests
{
    public class TestSceneMmlEvents : MmlTestScene
    {
        private const string TestData = "<mml>" +
                                        "    <Box Name=\"Box-1\" width=\"250\" height=\"250\" colour=\"#FF0000\" position=\"100,100\"/>" +
                                        "    <Box Name=\"Box-2\" width=\"100\" height=\"100\" colour=\"#FF00FF\" position=\"300,300\"/>" +
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

            var box1 = display.GetByName<Box>("Box-1");
            box1.RotateTo(360, 5000).Then(o => o.RotateTo(-360, 5000)).Loop();
            
            var box2 = display.GetByName<Box>("Box-2");
            box2.ScaleTo(2, 5000).Then(o => o.ScaleTo(0, 5000)).Loop();
        }
    }
}