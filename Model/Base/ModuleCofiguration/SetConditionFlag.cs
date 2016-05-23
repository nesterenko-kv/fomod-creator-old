using System;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     A condition flag to set if a Plugin is selected.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    [Serializable]
    public class SetConditionFlag
    {
        /// <summary>
        ///     The identifying name of the condition flag.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public string Value { get; set; }

        public static SetConditionFlag Create()
        {
            return new SetConditionFlag
            {
                Name = "is Flag Flag Flag",
                Value = "On"
            };
        }
    }
}