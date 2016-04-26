using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using FOMOD_Creator.Models.ModuleConfiguration.Dependencies;
using FOMOD_Creator.Models.ModuleConfiguration.Files;

namespace FOMOD_Creator.Models.ModuleConfiguration.Steps.Groups.Plugins.Conditions
{
    /// <summary>
    /// A pattern of mod files and conditional flags that determine whether to instal specific files.
    /// </summary>
    [Serializable]
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