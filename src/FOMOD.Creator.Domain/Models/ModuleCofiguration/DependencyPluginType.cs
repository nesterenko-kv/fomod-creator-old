namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;
    using PropertyChanged;

    /// <summary>
    ///     A <see cref="Plugin" /> type that is dependent upon the state of other
    ///     mods.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class DependencyPluginType
    {
        /// <summary>
        ///     The default type of the <see cref="Plugin" /> used if none of the
        ///     specified dependency states are satisfied.
        /// </summary>
        [XmlElement("defaultType")]
        public PluginType DefaultType { get; set; }

        /// <summary>
        ///     The list of dependency patterns against which to match the user's
        ///     installation. The first pattern that matches the user's installation
        ///     determines the type of the Plugin.
        /// </summary>
        [XmlArray("patterns")]
        [XmlArrayItem("pattern", IsNullable = false)]
        public ObservableCollection<DependencyPattern> Patterns { get; set; }

        public static DependencyPluginType Create()
        {
            return new DependencyPluginType
            {
                DefaultType = PluginType.Create()
            };
        }
    }
}
