using System;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     A Plugin.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    [Serializable]
    public class Plugin
    {

        protected Plugin()
        {
            TypeDescriptor = PluginTypeDescriptor.Create();
        }

        #region Properties

        /// <summary>
        ///     A description of the Plugin.
        /// </summary>
        [XmlElement("description")]
        public string Description { get; set; }

        /// <summary>
        ///     The optional image associated with a Plugin.
        /// </summary>
        [XmlElement("image")]
        public Image Image { get; set; }

        [XmlElement("files", typeof(FileList))]
        public FileList Files { get; set; }

        [XmlElement("conditionFlags", typeof(ConditionFlagList))]
        public ConditionFlagList ConditionFlags { get; set; }

        /// <summary>
        ///     Describes the type of the Plugin.
        /// </summary>
        [XmlElement("typeDescriptor")]
        public PluginTypeDescriptor TypeDescriptor { get; set; }

        /// <summary>
        ///     The name of the Plugin.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        #endregion

        public static Plugin Create()
        {
            return new Plugin
            {
                Name = "New Plugin"
            };
        }
    }
}