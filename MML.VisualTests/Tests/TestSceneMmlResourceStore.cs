using NUnit.Framework;
using osu.Framework.Allocation;

namespace MML.VisualTests.Tests
{
    public class TestSceneMmlResourceStore : MmlTestScene
    {
        [BackgroundDependencyLoader]
        private void Load(MmlStore store)
        {
            AddStep("Parse MML", () =>
            {
                var displayContainer = store.Get("MML/Box");

                Child = displayContainer;
            });
        }
    }
}