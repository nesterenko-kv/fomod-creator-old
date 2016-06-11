using System;
using System.ComponentModel;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspects;
using FomodModel.Base.ModuleCofiguration.Enum;

namespace FomodModel.Base
{
    /// <summary>
    ///     Contains all information about modification.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged)), Serializable, XmlRoot("fomod", Namespace = "", IsNullable = false)]
    public class ModuleInformation
    {
        public ModuleInformation()
        {
            CategoryId = CategoriesEnum.Category1;
        }

        #region Properties

        /// <summary>
        ///     The name of the mod.
        /// </summary>
        [XmlElement, DefaultValue(false)]
        public string Name { get; set; }

        /// <summary>
        ///     The version of the module.
        /// </summary>
        [XmlElement, DefaultValue(false)]
        public string Version { get; set; }

        /// <summary>
        ///     Author of module on nexus.
        /// </summary>
        [XmlElement, DefaultValue(false)]
        public string Author { get; set; }

        /// <summary>
        ///     Description of module.
        /// </summary>
        [XmlElement, DefaultValue(false)]
        public string Description { get; set; }

        /// <summary>
        ///     Link of module on nexus.
        /// </summary>
        [XmlElement, DefaultValue(false)]
        public string Website { get; set; }

        /// <summary>
        ///     The category id of the mod on nexus.
        /// </summary>
        [XmlElement, DefaultValue(CategoriesEnum.Category1)]
        public CategoriesEnum CategoryId { get; set; }

        #endregion
    }
}