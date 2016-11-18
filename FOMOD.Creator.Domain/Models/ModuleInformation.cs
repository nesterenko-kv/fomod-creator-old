namespace FOMOD.Creator.Domain.Models
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using FOMOD.Creator.Domain.Enums;
    using PropertyChanged;

    /// <summary>
    ///     Contains all information about modification.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    [XmlRoot("fomod", Namespace = "", IsNullable = false)]
    public class ModuleInformation
    {
        public ModuleInformation()
        {
            CategoryId = CategoriesEnum.Category1;
        }

        /// <summary>
        ///     The name of the mod.
        /// </summary>
        [XmlElement]
        [DefaultValue(false)]
        public string Name { get; set; }

        /// <summary>
        ///     The version of the module.
        /// </summary>
        [XmlElement]
        [DefaultValue(false)]
        public string Version { get; set; }

        /// <summary>
        ///     <para>
        ///         <see cref="FOMOD.Creator.Domain.Models.ModuleInformation.Author" />
        ///     </para>
        ///     <para>of module on nexus.</para>
        /// </summary>
        [XmlElement]
        [DefaultValue(false)]
        public string Author { get; set; }

        /// <summary>
        ///     <para>
        ///         <see cref="FOMOD.Creator.Domain.Models.ModuleInformation.Description" />
        ///     </para>
        ///     <para>of module.</para>
        /// </summary>
        [XmlElement]
        [DefaultValue(false)]
        public string Description { get; set; }

        /// <summary>
        ///     Link of module on nexus.
        /// </summary>
        [XmlElement]
        [DefaultValue(false)]
        public string Website { get; set; }

        /// <summary>
        ///     The category id of the mod on nexus.
        /// </summary>
        [XmlElement]
        [DefaultValue(CategoriesEnum.Category1)]
        public CategoriesEnum CategoryId { get; set; }
    }
}
