using System;
using System.Xml.Linq;
using MML.Factories;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Logging;

namespace MML
{
    public interface IMmlParser
    {
        Drawable ConstructContainer();

        T ParseAttribute<T>(XAttribute attribute, T def = default) where T : struct;
        object ParseAttribute(Type type, XAttribute attribute, object def = default);
        
        IReadOnlyDependencyContainer DependencyContainer { get; set; }
    }

    public class MmlParser : IMmlParser
    {
        private readonly XDocument _xdoc;
        private readonly IMmlValueParserFactory _parserFactory;
        private readonly IMmlObjectFactory _objectFactory;
        
        public IReadOnlyDependencyContainer DependencyContainer { get; set; }

        public MmlParser(string data, IMmlValueParserFactory parserFactory = null, IMmlObjectFactory objectFactory = null)
        {
            _xdoc = XDocument.Parse(data);

            _parserFactory = parserFactory ?? new MmlValueParserFactory();
            _objectFactory = objectFactory ?? new MmlObjectFactory(this);
        }

        public Drawable ConstructContainer()
        {
            if (_xdoc.Root == null || _xdoc.Root.Name.LocalName.ToLower() != "mml")
            {
                Logger.LogPrint("MML Root is non existent!", LoggingTarget.Runtime, LogLevel.Error);
                return null;
            }

            Drawable obj = null;

            if (_xdoc.Root != null)
            {
                obj = ConstructContainers(_xdoc.Root);

                obj.RelativeSizeAxes = Axes.Both;
                obj.Anchor = Anchor.Centre;
                obj.Origin = Anchor.Centre;
                obj.FillMode = FillMode.Stretch;
            }

            return obj ?? _objectFactory.Create(_xdoc.Root.Name.LocalName, _xdoc.Root);
        }

        private Drawable ConstructContainers(XElement element)
        {
            if (element == null)
                throw new NullReferenceException("No Root Container!");

            var obj = _objectFactory.Create(element.Name.LocalName, element);

            foreach (var e in element.Elements())
            {
                var childObject = ConstructContainers(e); // Construct Children
                
                obj.RelativeSizeAxes = Axes.Both;
                obj.Anchor = Anchor.Centre;
                obj.Origin = Anchor.Centre;
                obj.FillMode = FillMode.Stretch;

                (obj as Container<Drawable>)?.Add(childObject);
            }

            return obj;
        }

        public T ParseAttribute<T>(XAttribute attribute, T def = default) where T : struct
        {
            return (T)ParseAttribute(typeof(T), attribute, def);
        }

        public object ParseAttribute(Type type, XAttribute attribute, object def = default)
        {
            if (attribute == null)
                return def;

            if (type == typeof(string)) // This doesn't have to be in a value parser.
                return attribute.Value;

            if (type.IsEnum)
            {
                var parser = _parserFactory.CreateEnum();

                if (parser == null)
                    Logger.LogPrint($"Parser for type {type} was never implemented!", LoggingTarget.Runtime, LogLevel.Error);

                return parser?.Parse(type, attribute.Value) ?? def;
            }
            else
            {
                if (_parserFactory.DependencyContainer == null)
                    _parserFactory.DependencyContainer = DependencyContainer;
                
                var parserType = typeof(IMmlValueParser<>).MakeGenericType(type);
                var parser = (IMmlValueParser)_parserFactory.Create(type, parserType);

                if (parser == null)
                    Logger.LogPrint($"Parser for type {type} was never implemented!", LoggingTarget.Runtime, LogLevel.Error);

                return parser?.Parse(type, attribute.Value) ?? def;
            }
        }
    }
}
