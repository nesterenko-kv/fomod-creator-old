namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.Xml.Serialization;
    using PropertyChanged;

    /// <summary>
    ///     A condition flag to set if a <see cref="Plugin" /> is selected.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class SetConditionFlag
    {
        /// <summary>
        ///     The identifying name of the condition flag.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public string Value { get; set; }

        public static SetConditionFlag Create()
        {
            return new SetConditionFlag
            {
                Name = "Flag",
                Value = "On"
            };
        }
    }
}
