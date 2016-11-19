namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.Xml.Serialization;
    using PropertyChanged;

    /// <summary>
    ///     A pattern of mod files and conditional flags that determine whether to
    ///     instal specific files.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class ConditionalInstallPattern
    {
        /// <summary>
        ///     The list of mods and their states against which to match the user's
        ///     installation.
        /// </summary>
        [XmlElement("dependencies")]
        public CompositeDependency Dependencies { get; set; }

        /// <summary>
        ///     The files and filders to install if the pattern is matched.
        /// </summary>
        [XmlElement("files")]
        public FileList Files { get; set; }

        public static ConditionalInstallPattern Create()
        {
            return new ConditionalInstallPattern();
        }
    }
}
