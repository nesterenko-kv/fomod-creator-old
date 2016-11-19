namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.Xml.Serialization;
    using PropertyChanged;

    /// <summary>
    ///     A step in the install process containing groups of optional plugins.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class InstallStep
    {
        /// <summary>
        ///     The name of the install step.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        ///     The pattern against which to match the conditional flags and
        ///     installed files. If the pattern is matched, then the install step
        ///     will be visible.
        /// </summary>
        [XmlElement("visible")]
        public CompositeDependency Visible { get; set; }

        /// <summary>
        ///     The list of optional files (or plugins) that may optionally be
        ///     installed for this module.
        /// </summary>
        [XmlElement("optionalFileGroups")]
        public GroupList OptionalFileGroups { get; set; }


        public static InstallStep Create()
        {
            return new InstallStep
            {
                Name = "New Step"
            };
        }
    }
}
