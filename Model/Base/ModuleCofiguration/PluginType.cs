using System;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspects;
using FomodModel.Base.ModuleCofiguration.Enum;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     The type of a given Plugin.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged)), Serializable]
    public class PluginType
    {
        #region Properties

        /// <summary>
        ///     The name of the Plugin type.
        /// </summary>
        [XmlAttribute("name")]
        public PluginTypeEnum Name { get; set; }

        #endregion

        public static PluginType Create()
        {
            return new PluginType { Name = PluginTypeEnum.NotUsable };
        }
    }
}