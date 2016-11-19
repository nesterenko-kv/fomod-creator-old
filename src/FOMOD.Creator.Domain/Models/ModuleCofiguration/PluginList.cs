namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using FOMOD.Creator.Domain.Enums;
    using PropertyChanged;

    /// <summary>
    ///     A list of plugins.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class PluginList
    {
        public PluginList()
        {
            Order = OrderEnum.Ascending;
        }

        /// <summary>
        ///     The order by which to list the plugins.
        /// </summary>
        [XmlAttribute("order")]
        [DefaultValue(OrderEnum.Ascending)]
        public OrderEnum Order { get; set; }

        /// <summary>
        ///     A mod Plugin belonging to a Group.
        /// </summary>
        [XmlElement("plugin")]
        public ObservableCollection<Plugin> Plugin { get; set; }
    }
}
