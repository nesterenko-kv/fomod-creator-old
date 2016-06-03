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
    ///     A list of plugins.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged)), Serializable]
    public class PluginList
    {
        #region Properties

        /// <summary>
        ///     A mod Plugin belonging to a Group.
        /// </summary>
        [XmlElement("plugin")]
        public ObservableCollection<Plugin> Plugin { get; set; }

        /// <summary>
        ///     The order by which to list the plugins.
        /// </summary>
        [XmlAttribute("order"), DefaultValue(OrderEnum.Ascending)]
        public OrderEnum Order { get; set; }

        #endregion

        public PluginList()
        {
            Order = OrderEnum.Ascending;
        }

        public static PluginList Create()
        {
            return new PluginList();
        }
    }
}