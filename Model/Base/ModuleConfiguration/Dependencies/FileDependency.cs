using System;
using System.Xml.Serialization;
using FomodModel.Base.ModuleConfiguration.Enums;

namespace FomodModel.Base.ModuleConfiguration.Dependencies
{
    /// <summary>
    /// A mod upon which the type of a plugin depends.
    /// </summary>
    [Serializable]
    [AspectInjector.Broker.Aspect(typeof(FomodInfrastructure.Aspect.AspectINotifyPropertyChanged))]

    public class FileDependency
    {
        /// <summary>
        /// The file of the mod upon which a the plugin depends.
        /// </summary>
        [XmlAttribute("file")]
        public string File { get; set; }
        /// <summary>
        /// The state of the mod file.
        /// </summary>
        [XmlAttribute("state")]
        public FileDependencyState State { get; set; }
    }
}