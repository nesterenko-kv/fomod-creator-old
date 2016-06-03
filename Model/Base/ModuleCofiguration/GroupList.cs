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
    ///     A list of Plugin groups.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged)), Serializable]
    public class GroupList
    {
        #region Properties

        /// <summary>
        ///     A Group of plugins for the mod.
        /// </summary>
        [XmlElement("group")]
        public ObservableCollection<Group> Group { get; set; }

        /// <summary>
        ///     The order by which to list the groups.
        /// </summary>
        [XmlAttribute("order"), DefaultValue(OrderEnum.Ascending)]
        public OrderEnum Order { get; set; }

        #endregion

        public GroupList()
        {
            Order = OrderEnum.Ascending;
        }

        public static GroupList Create()
        {
            return new GroupList();
        }
    }
}