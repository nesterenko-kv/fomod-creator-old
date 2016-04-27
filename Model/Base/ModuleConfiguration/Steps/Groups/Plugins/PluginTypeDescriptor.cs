using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using FomodModel.Base.ModuleConfiguration.Dependencies;

namespace FomodModel.Base.ModuleConfiguration.Steps.Groups.Plugins
{
    /// <summary>
    /// Describes the type of a plugin.
    /// </summary>
    [Serializable]
    [AspectInjector.Broker.Aspect(typeof(FomodInfrastructure.Aspect.Aspect_INotifyPropertyChanged))]

    public class PluginTypeDescriptor
    {
        [XmlElement("dependencyType", typeof(DependencyPluginType), Form = XmlSchemaForm.Unqualified)]
        [XmlElement("type", typeof(PluginType), Form = XmlSchemaForm.Unqualified)]
        public object Item { get; set; }
    }
}