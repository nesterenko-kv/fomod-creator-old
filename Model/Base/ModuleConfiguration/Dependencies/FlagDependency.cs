using System;
using System.Xml.Serialization;

namespace FomodModel.Base.ModuleConfiguration.Dependencies
{
    /// <summary>
    /// A condition flag upon which the type of a plugin depends.
    /// </summary>
    [Serializable]
    public class FlagDependency
    {
        /// <summary>
        /// The name of the condition flag upon which a the plugin depends.
        /// </summary>
        [XmlAttribute("flag")]
        public string Flag { get; set; }
        /// <summary>
        /// The value of the condition flag upon which a the plugin depends.
        /// </summary>
        [XmlAttribute("value")]
        public string Value { get; set; }
    }
}