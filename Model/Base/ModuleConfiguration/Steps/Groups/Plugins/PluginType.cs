using System;
using System.Xml.Serialization;
using FomodModel.Base.ModuleConfiguration.Enums;

namespace FomodModel.Base.ModuleConfiguration.Steps.Groups.Plugins
{
    /// <summary>
    /// The type of a given plugin.
    /// </summary>
    [Serializable]
    [AspectInjector.Broker.Aspect(typeof(FomodInfrastructure.Aspect.Aspect_INotifyPropertyChanged))]

    public class PluginType
    {
        /// <summary>
        /// The name of the plugin type.
        /// </summary>
        [XmlAttribute("name")]
        public PluginTypeEnum Name { get; set; }
    }
}