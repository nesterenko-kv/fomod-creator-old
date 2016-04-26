using System;
using System.Xml.Serialization;

namespace FomodModel.Base.ModuleConfiguration.Dependencies
{
    /// <summary>
    /// A required minimum version of an item.
    /// </summary>
    [Serializable]
    public class VersionDependency
    {
        /// <summary>
        /// The required minimum version of the item.
        /// </summary>
        [XmlAttribute("version")]
        public string Version { get; set; }
    }
}