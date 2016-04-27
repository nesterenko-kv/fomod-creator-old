using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodModel.Base.Configuration.Enums;

namespace FomodModel.Base.Configuration.Steps
{
    [Aspect(typeof(AspectINotifyPropertyChanged))]
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