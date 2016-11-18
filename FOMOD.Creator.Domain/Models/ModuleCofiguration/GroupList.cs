namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;
    using FOMOD.Creator.Domain.Enums;
    using PropertyChanged;

    /// <summary>
    ///     A list of <see cref="Plugin" /> groups.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class GroupList
    {
        public GroupList()
        {
            Order = OrderEnum.Explicit;
        }

        /// <summary>
        ///     A Group of plugins for the mod.
        /// </summary>
        [XmlElement("group")]
        public ObservableCollection<Group> Group { get; set; }

        /// <summary>
        ///     The order by which to list the groups.
        /// </summary>
        [XmlAttribute("order")]
        public OrderEnum Order { get; set; }
    }
}
