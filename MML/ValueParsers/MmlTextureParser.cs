using System;
using MML.Factories;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;

namespace MML.ValueParsers
{
    public class MmlTextureParser : IMmlValueParser<Texture>
    {
        [Resolved]
        private TextureStore TextureStore { get; set; }
        
        public Texture Parse(string value)
        {
            return (Texture)Parse(typeof(Texture), value);
        }

        public IMmlValueParserFactory ParserFactory { get; set; }
        
        public object Parse(Type type, string value)
        {
            return TextureStore.Get(value);
        }
    }
}
