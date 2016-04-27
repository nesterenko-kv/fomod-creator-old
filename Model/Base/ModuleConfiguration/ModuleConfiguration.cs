using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using FomodModel.Base.ModuleConfiguration.Dependencies;
using FomodModel.Base.ModuleConfiguration.Files;
using FomodModel.Base.ModuleConfiguration.Header;
using FomodModel.Base.ModuleConfiguration.Steps;
using FomodModel.Base.ModuleConfiguration.Steps.Groups.Plugins.Conditions;

namespace FomodModel.Base.ModuleConfiguration
{
    /// <summary>
    /// Describes the configuration of a module.
    /// </summary>
    [AspectInjector.Broker.Aspect(typeof(FomodInfrastructure.Aspect.Aspect_INotifyPropertyChanged))]
    [Serializable]
    [XmlRoot("config", Namespace = "", IsNullable = false)]
    public class ModuleConfiguration
    {
        /// <summary>
        /// Add namespace.
        /// </summary>
        [XmlAttribute("noNamespaceSchemaLocation", Namespace = XmlSchema.InstanceNamespace)]
        public string Namespace = "http://qconsulting.ca/fo3/ModConfig5.0.xsd";

        /// <summary>
        /// The name of the module.
        /// </summary>
        [XmlElement("moduleName", Form = XmlSchemaForm.Unqualified)]
        public ModuleTitle ModuleName { get; set; }

        /// <summary>
        /// The module logo.
        /// </summary>
        [XmlElement("moduleImage", Form = XmlSchemaForm.Unqualified)]
        public HeaderImage ModuleImage { get; set; }

        /// <summary>
        /// Items upon which the module depends.
        /// </summary>
        [XmlElement("moduleDependencies", Form = XmlSchemaForm.Unqualified)]
        public CompositeDependency ModuleDependencies { get; set; }

        /// <summary>
        /// The list of files and folders that must be installed for this module.
        /// </summary>
        [XmlElement("requiredInstallFiles", Form = XmlSchemaForm.Unqualified)]
        public FileList RequiredInstallFiles { get; set; }

        /// <summary>
        /// The list of install steps that determine which files (or plugins) that may optionally be installed for this module.
        /// </summary>
        [XmlElement("installSteps", Form = XmlSchemaForm.Unqualified)]
        public StepList InstallSteps { get; set; }

        /// <summary>
        /// The list of optional files that may optionally be installed for this module, base on condition flags.
        /// </summary>
        [XmlElement("conditionalFileInstalls", Form = XmlSchemaForm.Unqualified)]
        public ConditionalFileInstallList ConditionalFileInstalls { get; set; }
    }
}