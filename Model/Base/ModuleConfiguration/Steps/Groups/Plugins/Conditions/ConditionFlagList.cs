using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FomodModel.Base.ModuleConfiguration.Steps.Groups.Plugins.Conditions
{
    /// <summary>
    /// A list of condition flags to set if a plugin is in the appropriate state.
    /// </summary>
    [Serializable]
    [AspectInjector.Broker.Aspect(typeof(FomodInfrastructure.Aspect.Aspect_INotifyPropertyChanged))]

    public class ConditionFlagList
    {
        [XmlElement("flag", Form = XmlSchemaForm.Unqualified)]
        public SetConditionFlag[] Flag { get; set; }
    }
}