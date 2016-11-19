namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.Xml.Serialization;
    using PropertyChanged;

    /// <summary>
    ///     A required minimum version of an item.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class VersionDependency
    {
        /// <summary>
        ///     The required minimum version of the item.
        /// </summary>
        [XmlAttribute("version")]
        public string Version { get; set; }
    }
}
