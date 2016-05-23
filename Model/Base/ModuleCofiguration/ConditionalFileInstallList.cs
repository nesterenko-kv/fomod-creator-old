using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     A list of optional files that may optionally be installed for this module, base on condition flags.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    [Serializable]
    public class ConditionalFileInstallList
    {
        /// <summary>
        ///     The list of patterns against which to match the conditional flags and installed files. All matching patterns will
        ///     have their files installed.
        /// </summary>
        [XmlArray("patterns")]
        [XmlArrayItem("pattern", IsNullable = false)]
        public ObservableCollection<ConditionalInstallPattern> Patterns { get; set; }



        public void AddPatern(ConditionalInstallPattern patern)
        {
            if (Patterns == null) Patterns = new ObservableCollection<ConditionalInstallPattern>();
            Patterns.Add(patern);
        }
        public void RemovePatern(ConditionalInstallPattern patern)
        {
            if (Patterns != null) Patterns.Remove(patern);
            if (Patterns.Count == 0) Patterns = null;
        }
    }
}