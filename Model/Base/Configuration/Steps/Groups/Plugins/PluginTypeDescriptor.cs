using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodModel.Base.Configuration.Dependencies;

namespace FomodModel.Base.Configuration.Steps.Groups.Plugins
{
    /// <summary>
    /// Describes the type of a plugin.
    /// </summary>
    [Serializable]
    [Aspect(typeof(AspectINotifyPropertyChanged))]

    public class PluginTypeDescriptor
    {
        [XmlElement("dependencyType", typeof(DependencyPluginType), Form = XmlSchemaForm.Unqualified)]
        [XmlElement("type", typeof(PluginType), Form = XmlSchemaForm.Unqualified)]
        public object Item { get; set; }
    }
}