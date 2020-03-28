using NUnit.Framework;

namespace MML.VisualTests.Tests
{
    public class TestSceneMmlSprite : MmlTestScene
    {
        private const string TestData = "<mml>" +
                                         "    <sprite texture=\"https://a.ppy.sh/10291354\" width=\"250\" height=\"250\"/>" +
                                         "</mml>";

        private const string BlurTestData = "<mml>" +
                                              "    <BufferedContainer blurSigma=\"4,-4\" cacheDrawnFrameBuffer=\"true\">" +
                                              "        <sprite texture=\"https://a.ppy.sh/10291354\" " +
                                              "            width=\"250\" " +
                                              "            height=\"250\" " +
                                              "        />" +
                                              "    </BufferedContainer>" +
                                              "</mml>";

        [Test]
        public void TestImage()
        {
            AddStep("Render Image", PerformImageRender);
            AddStep("Blur Image", PerformBlurImage);
        }

        private void PerformImageRender()
        {
            var parser = new MmlParser(TestData);
            var display = new MmlDisplayContainer(parser);

            Child = display;
        }

        private void PerformBlurImage()
        {
            var parser = new MmlParser(BlurTestData);
            var display = new MmlDisplayContainer(parser);

            Child = display;
        }
    }
}
