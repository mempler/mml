using System;
using JetBrains.Annotations;
using MML.Factories;

namespace MML.ValueParsers
{
    [UsedImplicitly]
    public class MmlFloatParser : IMmlValueParser<float>
    {
        public float Parse(string value)
        {
            return (float)Parse(typeof(float), value);
        }

        public IMmlValueParserFactory ParserFactory { get; set; }

        public object Parse(Type type, string value)
        {
            var doubleParser = ParserFactory.Create<double>();

            return (float)doubleParser.Parse(value);
        }
    }
}
