using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodModel.Base.Configuration.Steps.Groups.Plugins;

namespace FomodModel.Base.Configuration.Dependencies
{
    /// <summary>
    /// A pattern of mod files and condition flags that determine the type of a plugin.
    /// </summary>
    [Serializable]
    [Aspect(typeof(AspectINotifyPropertyChanged))]

    public class DependencyPattern
    {
        /// <summary>
        /// The list of mods and their states against which to match the user's installation.
        /// </summary>
        [XmlElement("dependencies", Form = XmlSchemaForm.Unqualified)]
        public CompositeDependency Dependencies { get; set; }
        
        /// <summary>
        /// The type of the plugin.
        /// </summary>
        [XmlElement("type", Form = XmlSchemaForm.Unqualified)]
        public PluginType Type { get; set; }
    }
}