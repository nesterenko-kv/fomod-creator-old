using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using FomodModel.Base.ModuleConfiguration.Enums;

namespace FomodModel.Base.ModuleConfiguration.Steps.Groups
{
    /// <summary>
    /// A list of plugin groups.
    /// </summary>
    [Serializable]
    public class GroupList
    {
        public GroupList()
        {
            Order = OrderEnum.Ascending;
        }

        /// <summary>
        /// A group of plugins for the mod.
        /// </summary>
        [XmlElement("group", Form = XmlSchemaForm.Unqualified)]
        public Group[] Group { get; set; }

        /// <summary>
        /// The order by which to list the steps.
        /// </summary>
        [XmlAttribute("order")]
        [DefaultValue(OrderEnum.Ascending)]
        public OrderEnum Order { get; set; }
    }
}