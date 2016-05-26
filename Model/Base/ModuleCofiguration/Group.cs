using System;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodModel.Base.ModuleCofiguration.Enum;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     A Group of plugins.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    [Serializable]
    public class Group
    {
        #region Properties

        /// <summary>
        ///     The list of plugins in the Group.
        /// </summary>
        [XmlElement("plugins")]
        public PluginList Plugins { get; set; }

        /// <summary>
        ///     The name of the Group.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        ///     The type of the Group.
        /// </summary>
        [XmlAttribute("type")]
        public GroupType Type { get; set; }

        #endregion

        public static Group Create()
        {
            return new Group
            {
                Name = "New Group",
                Type = GroupType.SelectAll
            };
        }
    }
}