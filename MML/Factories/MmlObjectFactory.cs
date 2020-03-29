using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace MML.Factories
{
    public interface IMmlObjectFactory
    {
        Drawable Create(string name, XElement element = null);
    }

    public class MmlObjectFactory : IMmlObjectFactory
    {
        private readonly MmlParser _parser;

        private readonly Dictionary<string, Type> _cachedObjectTypes =
            new Dictionary<string, Type>();

        public MmlObjectFactory(MmlParser parser)
        {
            _parser = parser;
        }

        public Drawable Create(string name, XElement element = null)
        {
            Drawable createdObject;

            if (_cachedObjectTypes.TryGetValue(name, out var cached))
            {
                var constructorInfos = cached.GetConstructors()
                    .Where(x => 
                        x.GetParameters().Length > 0 &&
                        x.GetParameters()
                            .Count(param => param.GetCustomAttributes(typeof(OptionalAttribute), false)
                                .Any()) == x.GetParameters().Length)
                    .ToArray();  
                
                if (constructorInfos.Length >= 1)
                {
                    var ctor = constructorInfos.First();
                    createdObject = (Drawable) ctor
                        .Invoke(BindingFlags.OptionalParamBinding | 
                                BindingFlags.InvokeMethod | 
                                BindingFlags.CreateInstance, 
                            null, 
                            ctor.GetParameters().Select(_ => Type.Missing).ToArray(), 
                            CultureInfo.InvariantCulture);
                }
                else
                {
                    createdObject = (Drawable) Activator.CreateInstance(cached);
                }
                
                ApplyAttributes(cached, createdObject, element);
                return createdObject;
            }

            var objectType = typeof(Drawable);

            var types =
                AppDomain.CurrentDomain.GetAssemblies()
                         .SelectMany(s => s.GetTypes())
                         .Where(p => objectType.IsAssignableFrom(p))
                         .Where(p => string.Equals(p.Name, name, StringComparison.CurrentCultureIgnoreCase))
                         .ToImmutableArray();

            if (!types.Any())
            {
                createdObject = new Container();
                ApplyAttributes(objectType, createdObject, element);
                return createdObject; // Create empty object if no Object with alias exists.
            }

            var objType = types.FirstOrDefault();
            _cachedObjectTypes[name] = objType;

            {
                var constructorInfos = objType.GetConstructors()
                    .Where(x => 
                        x.GetParameters().Length > 0 &&
                        x.GetParameters()
                            .Count(param => param.GetCustomAttributes(typeof(OptionalAttribute), false)
                                .Any()) == x.GetParameters().Length)
                    .ToArray();  
            
                if (constructorInfos.Length >= 1)
                {
                    var ctor = constructorInfos.First();
                    createdObject = (Drawable) ctor
                        .Invoke(BindingFlags.OptionalParamBinding | 
                                BindingFlags.InvokeMethod | 
                                BindingFlags.CreateInstance, 
                            null, 
                            ctor.GetParameters().Select(_ => Type.Missing).ToArray(), 
                            CultureInfo.InvariantCulture);
                }
                else
                {
                    createdObject = (Drawable) Activator.CreateInstance(objType);
                }
            }

            if (element == null)
                return createdObject;

            ApplyAttributes(objType, createdObject, element);

            return createdObject;
        }

        private void ApplyAttributes(Type objType, Drawable createdObject, XElement element)
        {
            var objProps = objType.GetProperties()
                                  .ToArray();

            createdObject.Name = element.Attribute("Name")?.Value ?? string.Empty;

            foreach (var objProp in objProps)
            {
                if (!objProp.CanWrite)
                    continue;

                var xamlAttribute = element
                                    .Attributes()
                                    .FirstOrDefault(a =>
                                        string.Equals(
                                            a.Name.LocalName,
                                            objProp.Name,
                                            StringComparison.CurrentCultureIgnoreCase
                                        )
                                    );

                if (xamlAttribute == null)
                    continue;

                var parsedAttribute = _parser.ParseAttribute(objProp.PropertyType, xamlAttribute);
                objProp.SetValue(createdObject, parsedAttribute);
            }
        }
    }
}
