using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodModel.Base.Configuration.Dependencies;
using FomodModel.Base.Configuration.Files;

namespace FomodModel.Base.Configuration.Steps.Groups.Plugins.Conditions
{
    /// <summary>
    /// A pattern of mod files and conditional flags that determine whether to instal specific files.
    /// </summary>
    [Serializable]
    [Aspect(typeof(AspectINotifyPropertyChanged))]

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