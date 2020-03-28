using System;
using JetBrains.Annotations;
using MML.Factories;
using osuTK;

namespace MML.ValueParsers
{
    [UsedImplicitly]
    public class MmlVector2Parser : IMmlValueParser<Vector2>
    {
        public Vector2 Parse(string value)
        {
            return (Vector2)Parse(typeof(Vector2), value);
        }

        public IMmlValueParserFactory ParserFactory { get; set; }

        public object Parse(Type type, string value)
        {
            var floatParser = ParserFactory.Create<float>();

            var data = value.Split(",");

            return data.Length switch
            {
                1 => new Vector2(floatParser.Parse(data[0])),
                2 => new Vector2(floatParser.Parse(data[0]), floatParser.Parse(data[1])),
                _ => Vector2.Zero
            };
        }
    }
}
