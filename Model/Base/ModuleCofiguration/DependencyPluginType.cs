using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     A Plugin type that is dependent upon the state of other mods.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged)), Serializable]
    public class DependencyPluginType
    {
        public static DependencyPluginType Create()
        {
            return new DependencyPluginType { DefaultType = PluginType.Create() };
        }

        #region Properties

        /// <summary>
        ///     The default type of the Plugin used if none of the specified dependency states are satisfied.
        /// </summary>
        [XmlElement("defaultType")]
        public PluginType DefaultType { get; set; }

        /// <summary>
        ///     The list of dependency patterns against which to match the user's installation. The first pattern that matches the
        ///     user's installation determines the type of the Plugin.
        /// </summary>
        [XmlArray("patterns"), XmlArrayItem("pattern", IsNullable = false)]
        public ObservableCollection<DependencyPattern> Patterns { get; set; }

        #endregion
    }
}