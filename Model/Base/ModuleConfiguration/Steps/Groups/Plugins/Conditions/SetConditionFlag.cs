using System;
using System.Xml.Serialization;

namespace FomodModel.Base.ModuleConfiguration.Steps.Groups.Plugins.Conditions
{
    /// <summary>
    /// >A condition flag to set if a plugin is selected.
    /// </summary>
    [Serializable]
    public class SetConditionFlag
    {
        /// <summary>
        /// The identifying name of the condition flag.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}