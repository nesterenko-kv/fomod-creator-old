using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     A list of condition flags to set if a Plugin is in the appropriate state.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    [Serializable]
    public class ConditionFlagList
    {
        /// <summary>
        ///     A condition flag to set if the Plugin is selected.
        /// </summary>
        [XmlElement("flag")]
        public ObservableCollection<SetConditionFlag> Flag { get; set; }
    }
}