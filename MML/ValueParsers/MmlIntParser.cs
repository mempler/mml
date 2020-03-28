using System;
using JetBrains.Annotations;
using MML.Factories;

namespace MML.ValueParsers
{
    [UsedImplicitly]
    public class MmlIntParser : IMmlValueParser<int>
    {
        public int Parse(string value)
        {
            return (int)Parse(typeof(int), value);
        }

        public IMmlValueParserFactory ParserFactory { get; set; }

        public object Parse(Type type, string value)
        {
            return int.TryParse(value, out var num) ? num : 0;
        }
    }
}
