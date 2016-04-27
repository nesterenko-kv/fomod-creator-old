using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using FomodModel.Base.ModuleConfiguration.Enums;

namespace FomodModel.Base.ModuleConfiguration.Steps
{
    [AspectInjector.Broker.Aspect(typeof(FomodInfrastructure.Aspect.Aspect_INotifyPropertyChanged))]
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