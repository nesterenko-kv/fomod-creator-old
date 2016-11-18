namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.Xml.Serialization;
    using FOMOD.Creator.Domain.Enums;
    using PropertyChanged;

    /// <summary>
    ///     A mod upon which the type of a <see cref="Plugin" /> depends.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class FileDependency
    {
        /// <summary>
        ///     The file of the mod upon which a the <see cref="Plugin" /> depends.
        /// </summary>
        [XmlAttribute("file")]
        public string File { get; set; }

        /// <summary>
        ///     The state of the mod file.
        /// </summary>
        [XmlAttribute("state")]
        public FileDependencyState State { get; set; }

        public static FileDependency Create()
        {
            return new FileDependency
            {
                File = "file.esm",
                State = FileDependencyState.Active
            };
        }
    }
}
