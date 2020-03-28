using System;
using System.Text.RegularExpressions;
using MML.Factories;
using MML.Utils;

namespace MML.ValueParsers
{
    public class MmlDoubleParser : IMmlValueParser<double>
    {
        private readonly Regex _nmRegex = new Regex(@"(\d+(\.\d+)?)");
        private readonly Regex _unitRegex = new Regex(@"[^\d\W]+");

        public double Parse(string value)
        {
            return (double)Parse(typeof(double), value);
        }

        public IMmlValueParserFactory ParserFactory { get; set; }

        public object Parse(Type type, string value)
        {
            var nm = _nmRegex.Match(value).Value;
            var unit = _unitRegex.Match(value).Value.ToLower();

            var f = double.TryParse(nm, out var num) ? num : 0f;

            var pixel = unit switch
            {
                "in" => UnitConverter.Inch2Pixel(f),
                "pt" => UnitConverter.Point2Pixel(f),
                "pc" => UnitConverter.Pica2Pixel(f),
                _ => f
            };

            return pixel;
        }
    }
}
