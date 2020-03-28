using System;
using JetBrains.Annotations;

namespace MML.ValueParsers
{
    [UsedImplicitly]
    public class MmlEnumParser : IMmlEnumParser
    {
        public TE Parse<TE>(string value)
            where TE : struct
        {
            return (TE)Parse(typeof(TE), value);
        }

        public object Parse(Type type, string value)
        {
            return Enum.TryParse(type, value, out var enm) ? enm : default;
        }
    }
}
