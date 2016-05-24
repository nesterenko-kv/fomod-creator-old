using System;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     A step in the install process containing groups of optional plugins.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    [Serializable]
    public class InstallStep
    {
        #region Properties
        
        /// <summary>
        ///     The pattern against which to match the conditional flags and installed files. If the pattern is matched, then the
        ///     install step will be visible.
        /// </summary>
        [XmlElement("visible")]
        public CompositeDependency Visible { get; set; }

        /// <summary>
        ///     The list of optional files (or plugins) that may optionally be installed for this module.
        /// </summary>
        [XmlElement("optionalFileGroups")]
        public GroupList OptionalFileGroups { get; set; }

        /// <summary>
        ///     The name of the install step.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        #endregion

        public static InstallStep Create()
        {
            return new InstallStep
            {
                Name = "New Step"
            };
        }
    }
}