namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.Xml.Serialization;
    using FOMOD.Creator.Domain.Enums;
    using PropertyChanged;

    /// <summary>
    ///     A <see cref="Group" /> of plugins.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class Group
    {
        /// <summary>
        ///     The name of the Group.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        ///     The list of plugins in the Group.
        /// </summary>
        [XmlElement("plugins")]
        public PluginList Plugins { get; set; }

        /// <summary>
        ///     The type of the Group.
        /// </summary>
        [XmlAttribute("type")]
        public GroupType Type { get; set; }

        public static Group Create()
        {
            return new Group
            {
                Name = "New Group",
                Type = GroupType.SelectAny
            };
        }
    }
}
