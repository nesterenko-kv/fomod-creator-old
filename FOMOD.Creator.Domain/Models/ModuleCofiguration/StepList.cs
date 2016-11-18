namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using FOMOD.Creator.Domain.Enums;
    using PropertyChanged;

    /// <summary>
    ///     A list of install steps.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class StepList
    {
        public StepList()
        {
            Order = OrderEnum.Ascending;
        }

        /// <summary>
        ///     A list of install steps for the mod.
        /// </summary>
        [XmlElement("installStep")]
        public ObservableCollection<InstallStep> InstallStep { get; set; }

        /// <summary>
        ///     The order by which to list the steps.
        /// </summary>
        [XmlAttribute("order")]
        [DefaultValue(OrderEnum.Ascending)]
        public OrderEnum Order { get; set; }
    }
}
