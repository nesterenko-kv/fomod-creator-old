using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using FOMOD_Creator.Models.ModuleConfiguration.Enums;

namespace FOMOD_Creator.Models.ModuleConfiguration.Steps
{
    [Serializable]
    public class StepList
    {
        public StepList()
        {
            Order = OrderEnum.Ascending;
        }

        [XmlElement("installStep", Form = XmlSchemaForm.Unqualified)]
        public InstallStep[] InstallStep { get; set; }

        [XmlAttribute("order")]
        [DefaultValue(OrderEnum.Ascending)]
        public OrderEnum Order { get; set; }
    }
}