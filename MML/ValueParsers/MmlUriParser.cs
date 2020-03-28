using System;
using MML.Factories;

namespace MML.ValueParsers
{
    public class MmlUriParser : IMmlValueParser<Uri>
    {
        public Uri Parse(string value)
        {
            return new Uri(value);
        }

        public IMmlValueParserFactory ParserFactory { get; set; }

        public object Parse(Type type, string value)
        {
            return new Uri(value);
        }
    }
}
