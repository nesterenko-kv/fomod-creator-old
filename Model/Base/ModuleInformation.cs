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
        /// <summary>
        /// The name of the mod.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The version of the module.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// The latest known version of the module.
        /// </summary>
        [DefaultValue(false)]
        public string LatestKnownVersion { get; set; }

        /// <summary>
        /// Identificator of module on nexus.
        /// </summary>
        [DefaultValue(false)]
        public int Id { get; set; }

        /// <summary>
        /// Author of module on nexus.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Identificator of module category on nexus.
        /// </summary>
        [DefaultValue(false)]
        public int CategoryId { get; set; }

        /// <summary>
        /// Identificator of module category in NNM.
        /// </summary>
        [DefaultValue(false)]
        public int CustomCategoryId { get; set; }

        /// <summary>
        /// Description of module.
        /// </summary>
        [DefaultValue(false)]
        public string Description { get; set; }

        [DefaultValue(false)]
        public string UpdateWarningEnabled { get; set; }

        /// <summary>
        /// Link of module on nexus.
        /// </summary>
        [DefaultValue(false)]
        public string Website { get; set; }
    }
}