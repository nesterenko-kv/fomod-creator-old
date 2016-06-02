using System;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodModel.Base.ModuleCofiguration.Enum;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     A pattern of mod files and condition flags that determine the type of a Plugin.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged)), Serializable]
    public class DependencyPattern
    {
        #region Properties

        /// <summary>
        ///     The list of mods and their states against which to match the user's installation.
        /// </summary>
        [XmlElement("dependencies")]
        public CompositeDependency Dependencies { get; set; }

        /// <summary>
        ///     The type of the Plugin.
        /// </summary>
        [XmlElement("type")]
        public PluginType Type { get; set; }

        public static DependencyPattern Create()
        {
            return new DependencyPattern { Type = new PluginType { Name = PluginTypeEnum.NotUsable } };
        }

        #endregion
    }
}