using System;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     A required minimum version of an item.
    /// </summary>
    [Aspect(typeof (AspectINotifyPropertyChanged))]
    [Serializable]
    public class VersionDependency
    {
        #region Properties

        /// <summary>
        ///     The required minimum version of the item.
        /// </summary>
        [XmlAttribute("version")]
        public string Version { get; set; }
        
        #endregion
    }
}