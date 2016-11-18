namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.Xml.Serialization;
    using PropertyChanged;

    /// <summary>
    ///     A Plugin.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class Plugin
    {
        protected Plugin()
        {
            TypeDescriptor = PluginTypeDescriptor.Create();
        }

        /// <summary>
        ///     A description of the Plugin.
        /// </summary>
        [XmlElement("description")]
        public string Description { get; set; }

        /// <summary>
        ///     The optional image associated with a Plugin.
        /// </summary>
        [XmlElement("image")]
        public Image Image { get; set; }

        [XmlElement("files", typeof(FileList))]
        public FileList Files { get; set; }

        [XmlElement("conditionFlags", typeof(ConditionFlagList))]
        public ConditionFlagList ConditionFlags { get; set; }


        /// <summary>
        ///     The name of the Plugin.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Describes the type of the Plugin.
        /// </summary>
        [XmlElement("typeDescriptor")]
        public PluginTypeDescriptor TypeDescriptor { get; set; }

        public static Plugin Create()
        {
            return new Plugin
            {
                Name = "New Plugin"
            };
        }
    }
}
