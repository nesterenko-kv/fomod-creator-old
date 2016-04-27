using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using FomodModel.Base.ModuleConfiguration.Dependencies;
using FomodModel.Base.ModuleConfiguration.Steps.Groups;

namespace FomodModel.Base.ModuleConfiguration.Steps
{
    /// <summary>
    /// A step in the install process containing groups of optional plugins.
    /// </summary>
    [AspectInjector.Broker.Aspect(typeof(FomodInfrastructure.Aspect.AspectINotifyPropertyChanged))]
    [Serializable]
    public class InstallStep
    {
        /// <summary>
        /// The pattern against which to match the conditional flags and installed files. If the pattern is matched, then the install step will be visible.
        /// </summary>
        [XmlElement("visible", Form = XmlSchemaForm.Unqualified)]
        public CompositeDependency Visible { get; set; }

        /// <summary>
        /// The list of optional files (or plugins) that may optionally be installed for this module.
        /// </summary>
        [XmlElement("optionalFileGroups", Form = XmlSchemaForm.Unqualified)]
        public GroupList OptionalFileGroups { get; set; }

        /// <summary>
        /// The name of the install step.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}