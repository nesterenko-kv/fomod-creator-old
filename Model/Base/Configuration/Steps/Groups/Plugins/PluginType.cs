using System;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodModel.Base.Configuration.Enums;

namespace FomodModel.Base.Configuration.Steps.Groups.Plugins
{
    /// <summary>
    /// The type of a given plugin.
    /// </summary>
    [Serializable]
    [Aspect(typeof(AspectINotifyPropertyChanged))]

    public class PluginType
    {
        /// <summary>
        /// The name of the plugin type.
        /// </summary>
        [XmlAttribute("name")]
        public PluginTypeEnum Name { get; set; }
    }
}