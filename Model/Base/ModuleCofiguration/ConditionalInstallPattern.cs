using System;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     A pattern of mod files and conditional flags that determine whether to instal specific files.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    [Serializable]
    public class ConditionalInstallPattern
    {
        /// <summary>
        ///     The list of mods and their states against which to match the user's installation.
        /// </summary>
        [XmlElement("dependencies")]
        public CompositeDependency Dependencies { get; set; }

        /// <summary>
        ///     The files and filders to install if the pattern is matched.
        /// </summary>
        [XmlElement("files")]
        public FileList Files { get; set; } = new FileList();
    }
}