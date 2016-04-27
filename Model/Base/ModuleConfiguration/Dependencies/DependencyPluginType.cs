using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using FomodModel.Base.ModuleConfiguration.Steps.Groups.Plugins;

namespace FomodModel.Base.ModuleConfiguration.Dependencies
{
    /// <summary>
    /// A plugin type that is dependent upon the state of other mods.
    /// </summary>
    [Serializable]
    [AspectInjector.Broker.Aspect(typeof(FomodInfrastructure.Aspect.AspectINotifyPropertyChanged))]

    public class DependencyPluginType
    {
        /// <summary>
        /// The default type of the plugin used if none of the specified dependency states are satisfied.
        /// </summary>
        [XmlElement("defaultType", Form = XmlSchemaForm.Unqualified)]
        public PluginType DefaultType { get; set; }

        /// <summary>
        /// The list of dependency patterns against which to match the user's installation. The first pattern that matches the user's installation determines the type of the plugin.
        /// </summary>
        [XmlArray("patterns", Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItem("pattern", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public DependencyPattern[] Patterns { get; set; }
    }
}