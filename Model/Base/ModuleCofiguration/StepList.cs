using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodModel.Base.ModuleCofiguration.Enum;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     A list of install steps.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged)), Serializable]
    public class StepList
    {
        #region Properties

        /// <summary>
        ///     A list of install steps for the mod.
        /// </summary>
        [XmlElement("installStep")]
        public ObservableCollection<InstallStep> InstallStep { get; set; }

        /// <summary>
        ///     The order by which to list the steps.
        /// </summary>
        [XmlAttribute("order"), DefaultValue(OrderEnum.Ascending)]
        public OrderEnum Order { get; set; }

        #endregion

        public StepList()
        {
            Order = OrderEnum.Ascending;
        }

        public static StepList Create()
        {
            return new StepList();
        }
    }
}