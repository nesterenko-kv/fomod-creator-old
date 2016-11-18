namespace FOMOD.Creator.Domain.Models
{
    using System;
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using FOMOD.Creator.Domain.Models.ModuleCofiguration;
    using PropertyChanged;

    /// <summary>
    ///     Describes the configuration of a module.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    [XmlRoot("config", Namespace = "", IsNullable = false)]
    public class ModuleConfiguration
    {
        /// <summary>
        ///     Add namespace.
        /// </summary>
        [XmlAttribute("noNamespaceSchemaLocation", Namespace = XmlSchema.InstanceNamespace)]
        public string Namespace = "http://qconsulting.ca/fo3/ModConfig5.0.xsd";

        /// <summary>
        ///     Items upon which the module depends.
        /// </summary>
        [XmlElement("moduleDependencies")]
        public CompositeDependency ModuleDependencies { get; set; }

        /// <summary>
        ///     The list of files and folders that must be installed for this
        ///     module.
        /// </summary>
        [XmlElement("requiredInstallFiles")]
        public FileList RequiredInstallFiles { get; set; }

        /// <summary>
        ///     The list of install steps that determine which files (or plugins)
        ///     that may optionally be installed for this module.
        /// </summary>
        [XmlElement("installSteps")]
        public StepList InstallSteps { get; set; }

        /// <summary>
        ///     The list of optional files that may optionally be installed for this
        ///     module, base on condition flags.
        /// </summary>
        [XmlElement("conditionalFileInstalls")]
        public ConditionalFileInstallList ConditionalFileInstalls { get; set; }


        /// <summary>
        ///     The name of the module.
        /// </summary>
        [XmlElement("moduleName")]
        public ModuleTitle ModuleName { get; set; }

        /// <summary>
        ///     The module logo.
        /// </summary>
        [XmlElement("moduleImage")]
        public HeaderImage ModuleImage { get; set; }
    }
}
