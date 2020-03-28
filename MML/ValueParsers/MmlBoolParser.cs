using System;
using MML.Factories;

namespace MML.ValueParsers
{
    public class MmlBoolParser : IMmlValueParser<bool>
    {
        public bool Parse(string value)
        {
            return (bool)Parse(typeof(bool), value);
        }

        public IMmlValueParserFactory ParserFactory { get; set; }

        public object Parse(Type type, string value)
        {
            return bool.TryParse(value, out var b) && b;
        }
    }
}
