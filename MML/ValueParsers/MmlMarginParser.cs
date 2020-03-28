using System;
using MML.Factories;
using osu.Framework.Graphics;

namespace MML.ValueParsers
{
    public class MmlMarginParser : IMmlValueParser<MarginPadding>
    {
        public MarginPadding Parse(string value)
        {
            return (MarginPadding)Parse(typeof(MarginPadding), value);
        }

        public IMmlValueParserFactory ParserFactory { get; set; }

        public object Parse(Type type, string value)
        {
            var floatParser = ParserFactory.Create<float>();

            var padding = new MarginPadding();

            var paddingValues = value.Split(',');

            // TODO: Implement auto keyword. as i have no clue how to implement it at this point of time.
            switch (paddingValues.Length)
            {
                case 1:
                    padding = new MarginPadding(floatParser.Parse(paddingValues[0]));
                    break;

                case 2:
                    padding.Top = floatParser.Parse(paddingValues[0]);
                    padding.Bottom = floatParser.Parse(paddingValues[1]);
                    break;

                case 3:
                    padding.Top = floatParser.Parse(paddingValues[0]);
                    padding.Bottom = floatParser.Parse(paddingValues[1]);
                    padding.Left = floatParser.Parse(paddingValues[2]);
                    break;

                case 4:
                    padding.Top = floatParser.Parse(paddingValues[0]);
                    padding.Bottom = floatParser.Parse(paddingValues[1]);
                    padding.Left = floatParser.Parse(paddingValues[2]);
                    padding.Right = floatParser.Parse(paddingValues[3]);
                    break;
            }

            return padding;
        }
    }
}
