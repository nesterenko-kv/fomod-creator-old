using System;
using System.ComponentModel;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace FomodModel.Base
{
    /// <summary>
    /// Contains all information about modification.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    [Serializable]
    [XmlRoot("fomod", Namespace = "", IsNullable = false)]
    public class ModuleInformation
    {
        public ModuleInformation()
        {
            CategoryId = CategoriesEnum.None;
        }

        #region Properties

        /// <summary>
        /// The name of the mod.
        /// </summary>
        [XmlElement]
        [DefaultValue(false)]
        public string Name { get; set; }

        /// <summary>
        /// The version of the module.
        /// </summary>
        [XmlElement]
        [DefaultValue(false)]
        public string Version { get; set; }

        /// <summary>
        /// Author of module on nexus.
        /// </summary>
        [XmlElement]
        [DefaultValue(false)]
        public string Author { get; set; }

        /// <summary>
        /// Description of module.
        /// </summary>
        [XmlElement]
        [DefaultValue(false)]
        public string Description { get; set; }

        /// <summary>
        /// Link of module on nexus.
        /// </summary>
        [XmlElement]
        [DefaultValue(false)]
        public string Website { get; set; }


        /// <summary>
        /// The category id of the mod on nexus.
        /// </summary>
        [XmlElement]
        [DefaultValue(CategoriesEnum.None)]
        public CategoriesEnum CategoryId { get; set; }

        #endregion
    }

    [Serializable]
    public enum CategoriesEnum
    {
        [XmlEnum("0")]
        None,

        [XmlEnum("2")]
        Category2,

        [XmlEnum("3")]
        Category3,

        [XmlEnum("4")]
        Category4,

        [XmlEnum("5")]
        Category5,

        [XmlEnum("6")]
        Category6,

        [XmlEnum("7")]
        Category7,

        [XmlEnum("8")]
        Category8,

        [XmlEnum("9")]
        Category9,

        [XmlEnum("10")]
        Category10,

        [XmlEnum("11")]
        Category11,

        [XmlEnum("12")]
        Category12,

        [XmlEnum("13")]
        Category13,

        [XmlEnum("14")]
        Category14,

        [XmlEnum("15")]
        Category15,

        [XmlEnum("16")]
        Category16,

        [XmlEnum("17")]
        Category17,

        [XmlEnum("18")]
        Category18,

        [XmlEnum("19")]
        Category19,

        [XmlEnum("20")]
        Category20,

        [XmlEnum("21")]
        Category21,

        [XmlEnum("22")]
        Category22,

        [XmlEnum("23")]
        Category23,

        [XmlEnum("24")]
        Category24,

        [XmlEnum("25")]
        Category25,

        [XmlEnum("26")]
        Category26,

        [XmlEnum("27")]
        Category27,

        [XmlEnum("28")]
        Category28,

        [XmlEnum("29")]
        Category29,

        [XmlEnum("30")]
        Category30,

        [XmlEnum("31")]
        Category31,

        [XmlEnum("32")]
        Category32,

        [XmlEnum("33")]
        Category33,

        [XmlEnum("34")]
        Category34,

        [XmlEnum("35")]
        Category35,

        [XmlEnum("36")]
        Category36,

        [XmlEnum("37")]
        Category37,

        [XmlEnum("38")]
        Category38,

        [XmlEnum("39")]
        Category39,

        [XmlEnum("40")]
        Category40,

        [XmlEnum("41")]
        Category41,

        [XmlEnum("42")]
        Category42,

        [XmlEnum("43")]
        Category43,

        [XmlEnum("44")]
        Category44,

        [XmlEnum("45")]
        Category45,

        [XmlEnum("46")]
        Category46,

        [XmlEnum("47")]
        Category47,

        [XmlEnum("48")]
        Category48,
    }
}