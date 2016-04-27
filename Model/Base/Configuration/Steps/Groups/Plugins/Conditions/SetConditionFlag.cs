using System;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace FomodModel.Base.Configuration.Steps.Groups.Plugins.Conditions
{
    /// <summary>
    /// >A condition flag to set if a plugin is selected.
    /// </summary>
    [Serializable]
    [Aspect(typeof(AspectINotifyPropertyChanged))]

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