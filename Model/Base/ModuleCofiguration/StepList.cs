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
    [Aspect(typeof (AspectINotifyPropertyChanged))]
    [Serializable]
    public class StepList
    {
        /// <summary>
        ///     StepList class constructor
        /// </summary>
        public StepList()
        {
            Order = OrderEnum.Ascending;
        }

        /// <summary>
        ///     A list of install steps for the mod.
        /// </summary>
        [XmlElement("installStep")]
        public ObservableCollection<InstallStep> InstallStep { get; set; }

        /// <summary>
        ///     The order by which to list the steps.
        /// </summary>
        [XmlAttribute("order")]
        [DefaultValue(OrderEnum.Ascending)]
        public OrderEnum Order { get; set; }

        internal static StepList Create()
        {
            return new StepList
            {
                InstallStep = new ObservableCollection<ModuleCofiguration.InstallStep>()
            };
        }

        public ObservableCollection<InstallStep> GetInstallStep()
        {
            if (InstallStep == null)
                InstallStep = new ObservableCollection<ModuleCofiguration.InstallStep>();
            return InstallStep;
        }
    }
}