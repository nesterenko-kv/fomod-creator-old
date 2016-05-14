using System;
using System.ComponentModel;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     A file or folder that may be installed as part of a module or Plugin.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    [Serializable]
    public abstract class SystemItem
    {
        /// <summary>
        ///     FileSystemItem class constructor
        /// </summary>
        protected SystemItem()
        {
            AlwaysInstall = false;
            InstallIfUsable = false;
            Priority = "0";
        }

        /// <summary>
        ///     The path to the file or folder in the FOMod.
        /// </summary>
        [XmlAttribute("source")]
        public string Source { get; set; }

        /// <summary>
        ///     The path to which the file or folder should be installed. If omitted, the destination is the same as the source.
        /// </summary>
        [XmlAttribute("destination")]
        public string Destination { get; set; }

        /// <summary>
        ///     Indicates that the file or folder should always be installed, regardless of whether or not the Plugin has been
        ///     selected.
        /// </summary>
        [XmlAttribute("alwaysInstall")]
        [DefaultValue(false)]
        public bool AlwaysInstall { get; set; }

        /// <summary>
        ///     Indicates that the file or folder should always be installed if the Plugin is not NotUsable, regardless of whether
        ///     or not the Plugin has been selected.
        /// </summary>
        [XmlAttribute("installIfUsable")]
        [DefaultValue(false)]
        public bool InstallIfUsable { get; set; }

        /// <summary>
        ///     A number describing the relative priority of the file or folder. A higher number indicates the file or folder
        ///     should be installed after the items with lower numbers. This value does not have to be unique.
        /// </summary>
        [XmlAttribute("priority", DataType = "integer")]
        [DefaultValue("0")]
        public string Priority { get; set; }
    }
}