namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.Xml.Serialization;
    using PropertyChanged;

    /// <summary>
    ///     A condition flag upon which the type of a <see cref="Plugin" /> depends.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class FlagDependency
    {
        /// <summary>
        ///     <para>
        ///         The name of the condition flag upon which a the
        ///         <see cref="Plugin" />
        ///     </para>
        ///     <para>depends.</para>
        /// </summary>
        [XmlAttribute("flag")]
        public string Flag { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }

        public static FlagDependency Create()
        {
            return new FlagDependency
            {
                Flag = "Flag",
                Value = "On"
            };
        }
    }
}
