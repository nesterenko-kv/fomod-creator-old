using System;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace FomodModel.Base.Configuration.Dependencies
{
    /// <summary>
    /// A required minimum version of an item.
    /// </summary>
    [Serializable]
    [Aspect(typeof(AspectINotifyPropertyChanged))]

    public class VersionDependency
    {
        /// <summary>
        /// The required minimum version of the item.
        /// </summary>
        [XmlAttribute("version")]
        public string Version { get; set; }
    }
}