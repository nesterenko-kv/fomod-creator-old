namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;
    using PropertyChanged;

    /// <summary>
    ///     A list of optional files that may optionally be installed for this
    ///     module, base on condition flags.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class ConditionalFileInstallList
    {
        /// <summary>
        ///     The list of patterns against which to match the conditional flags
        ///     and installed files. All matching patterns will have their files
        ///     installed.
        /// </summary>
        [XmlArray("patterns")]
        [XmlArrayItem("pattern", IsNullable = false)]
        public ObservableCollection<ConditionalInstallPattern> Patterns { get; set; }

        public static ConditionalFileInstallList Create()
        {
            return new ConditionalFileInstallList
            {
                Patterns = new ObservableCollection<ConditionalInstallPattern>()
            };
        }
    }
}
