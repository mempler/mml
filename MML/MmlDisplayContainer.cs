using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;

namespace MML
{
    public sealed class MmlDisplayContainer : Container
    {
        private readonly IMmlParser _parser;

        public MmlDisplayContainer(IMmlParser parser)
        {
            RelativeSizeAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            FillMode = FillMode.Stretch;
            
            _parser = parser;
        }

        [BackgroundDependencyLoader]
        private void Load()
        {
            _parser.DependencyContainer = Dependencies;
            Dependencies.Inject(_parser);
            
            Add(_parser.ConstructContainer());
        }
    }
}
