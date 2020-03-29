using System.Linq;
using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

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

        // this will fail if too many nested objects. need optimization.
        private static Drawable StackableLoop(string name, Drawable drawable)
        {
            if (drawable.Name == name)
                return drawable;
            
            var children = (drawable as Container<Drawable>)?.Children;
            return children?.Select(child => StackableLoop(name, child)).FirstOrDefault(result => result != null);
        }

        [CanBeNull]
        public T GetByName<T>(string name) where T : Drawable
        {
            return (T) StackableLoop(name, this);
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
