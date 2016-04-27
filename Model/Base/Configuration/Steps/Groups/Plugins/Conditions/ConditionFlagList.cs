using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace FomodModel.Base.Configuration.Steps.Groups.Plugins.Conditions
{
    /// <summary>
    /// A list of condition flags to set if a plugin is in the appropriate state.
    /// </summary>
    [Serializable]
    [Aspect(typeof(AspectINotifyPropertyChanged))]

    public class ConditionFlagList
    {
        [XmlElement("flag", Form = XmlSchemaForm.Unqualified)]
        public SetConditionFlag[] Flag { get; set; }
    }
}