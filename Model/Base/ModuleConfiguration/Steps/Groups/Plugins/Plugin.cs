using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using FomodModel.Base.ModuleConfiguration.Files;
using FomodModel.Base.ModuleConfiguration.Steps.Groups.Plugins.Conditions;

namespace FomodModel.Base.ModuleConfiguration.Steps.Groups.Plugins
{
    /// <summary>
    /// A plugin.
    /// </summary>
    [Serializable]
    [AspectInjector.Broker.Aspect(typeof(FomodInfrastructure.Aspect.AspectINotifyPropertyChanged))]

    public class Plugin
    {
        /// <summary>
        /// A description of the plugin.
        /// </summary>
        [XmlElement("description", Form = XmlSchemaForm.Unqualified)]
        public string Description { get; set; }

        /// <summary>
        /// The optional image associated with a plugin.
        /// </summary>
        [XmlElement("image", Form = XmlSchemaForm.Unqualified)]
        public Image Image { get; set; }

        /// <summary>
        /// The list of files and folders that need to be installed for the plugin and condition flags to set if the plugin is in the appropriate state.
        /// </summary>
        [XmlElement("conditionFlags", typeof(ConditionFlagList), Form = XmlSchemaForm.Unqualified)]
        [XmlElement("files", typeof(FileList), Form = XmlSchemaForm.Unqualified)]
        public object[] Items { get; set; }

        /// <summary>
        /// Describes the type of the plugin.
        /// </summary>
        [XmlElement("typeDescriptor", Form = XmlSchemaForm.Unqualified)]
        public PluginTypeDescriptor TypeDescriptor { get; set; }

        /// <summary>
        /// The name of the plugin.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}