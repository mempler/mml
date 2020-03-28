using System;
using JetBrains.Annotations;
using MML.Factories;

namespace MML
{
    [UsedImplicitly]
    public interface IMmlValueParser
    {
        IMmlValueParserFactory ParserFactory { get; set; }
        object Parse(Type type, string value);
    }

    [UsedImplicitly]
    public interface IMmlValueParser<out T> : IMmlValueParser
    {
        T Parse(string value);
    }

    public interface IMmlEnumParser
    {
        TE Parse<TE>(string value) where TE : struct;
        object Parse(Type type, string value);
    }
}
