using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodModel.Base.Configuration.Dependencies;
using FomodModel.Base.Configuration.Files;
using FomodModel.Base.Configuration.Header;
using FomodModel.Base.Configuration.Steps;
using FomodModel.Base.Configuration.Steps.Groups.Plugins.Conditions;

namespace FomodModel.Base
{
    /// <summary>
    /// Describes the configuration of a module.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged))]
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