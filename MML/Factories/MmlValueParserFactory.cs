using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;

namespace MML.Factories
{
    public interface IMmlValueParserFactory
    {
        IMmlValueParser<T> Create<T>();

        IMmlEnumParser CreateEnum();

        object Create(Type type, Type parserType);
        
        IReadOnlyDependencyContainer DependencyContainer { get; set; }
    }

    public class MmlValueParserFactory : IMmlValueParserFactory
    {
        private readonly Dictionary<Type, object> _cachedParsers =
            new Dictionary<Type, object>();

        public IReadOnlyDependencyContainer DependencyContainer { get; set; }
        
        public IMmlValueParser<T> Create<T>()
        {
            var parser = (IMmlValueParser<T>)Create(typeof(T), typeof(IMmlValueParser<T>));
            parser.ParserFactory = this;
            return parser;
        }

        public IMmlEnumParser CreateEnum()
        {
            return (IMmlEnumParser)Create(typeof(Enum), typeof(IMmlEnumParser));
        }

        public object Create(Type type, Type parserType)
        {
            if (_cachedParsers.TryGetValue(type, out var cached))
                return cached;

            var interfaceType = parserType;
            var types =
                AppDomain.CurrentDomain
                         .GetAssemblies()
                         .SelectMany(s => s.GetTypes())
                         .Where(p => interfaceType.IsAssignableFrom(p)).ToList();

            var parser = types.LastOrDefault();
            if (parser == null)
                return null;

            var instance = Activator.CreateInstance(parser);
            parser.GetProperty("ParserFactory")?.SetValue(instance, this);
            DependencyContainer.Inject(instance);
            _cachedParsers[type] = instance;

            return instance;
        }
    }
}
