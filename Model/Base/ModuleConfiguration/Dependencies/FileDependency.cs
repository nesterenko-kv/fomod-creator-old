using System;
using System.Xml.Serialization;
using FOMOD_Creator.Models.ModuleConfiguration.Enums;

namespace FOMOD_Creator.Models.ModuleConfiguration.Dependencies
{
    /// <summary>
    /// A mod upon which the type of a plugin depends.
    /// </summary>
    [Serializable]
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