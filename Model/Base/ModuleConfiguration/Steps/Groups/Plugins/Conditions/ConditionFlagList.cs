using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FOMOD_Creator.Models.ModuleConfiguration.Steps.Groups.Plugins.Conditions
{
    /// <summary>
    /// A list of condition flags to set if a plugin is in the appropriate state.
    /// </summary>
    [Serializable]
    public class ConditionFlagList
    {
        [XmlElement("flag", Form = XmlSchemaForm.Unqualified)]
        public SetConditionFlag[] Flag { get; set; }
    }
}