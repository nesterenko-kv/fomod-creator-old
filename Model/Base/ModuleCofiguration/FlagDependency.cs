using System;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     A condition flag upon which the type of a Plugin depends.
    /// </summary>
    [Aspect(typeof (AspectINotifyPropertyChanged))]
    [Serializable]
    public class FlagDependency
    {
        /// <summary>
        ///     The name of the condition flag upon which a the Plugin depends.
        /// </summary>
        [XmlAttribute("flag")]
        public string Flag { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlIgnore]
        public CompositeDependency Parent { get; set; }

        public static FlagDependency Create()
        {
            return new FlagDependency
            {
                Flag = "is Flag Flag Flag",
                Value = "On"
            };
        }
    }
}