using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using FomodModel.Base.ModuleConfiguration.Dependencies;
using FomodModel.Base.ModuleConfiguration.Files;

namespace FomodModel.Base.ModuleConfiguration.Steps.Groups.Plugins.Conditions
{
    /// <summary>
    /// A pattern of mod files and conditional flags that determine whether to instal specific files.
    /// </summary>
    [Serializable]
    [AspectInjector.Broker.Aspect(typeof(FomodInfrastructure.Aspect.AspectINotifyPropertyChanged))]

    public class ConditionalInstallPattern
    {
        /// <summary>
        /// The list of mods and their states against which to match the user's installation.
        /// </summary>
        [XmlElement("dependencies", Form = XmlSchemaForm.Unqualified)]
        public CompositeDependency Dependencies { get; set; }

        /// <summary>
        /// The files and filders to install if the pattern is matched.
        /// </summary>
        [XmlElement("files", Form = XmlSchemaForm.Unqualified)]
        public FileList Files { get; set; }
    }
}