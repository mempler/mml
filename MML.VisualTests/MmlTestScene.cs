using osu.Framework.Allocation;
using osu.Framework.IO.Stores;
using osu.Framework.Testing;

namespace MML.VisualTests
{
    public class MmlTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new FrameworkTestSceneTestRunner();

        private class FrameworkTestSceneTestRunner : TestSceneTestRunner
        {
            [BackgroundDependencyLoader]
            private void Load()
            {
                Resources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore(typeof(MmlTestScene).Assembly), "Resources"));
            }
        }
    }
}