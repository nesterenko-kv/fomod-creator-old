namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using FOMOD.Creator.Domain.Enums;
    using PropertyChanged;

    /// <summary>
    ///     Describes the display properties of the module title.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class ModuleTitle
    {
        public ModuleTitle()
        {
            Position = ModuleTitlePosition.Left;
            Colour = "000000";
        }

        /// <summary>
        ///     The colour to use for the title."hexBinary"
        /// </summary>
        [XmlAttribute("colour")]
        [DefaultValue("000000")]
        public string Colour { get; set; }

        /// <summary>
        ///     The identifying name of the condition flag.
        /// </summary>
        [XmlAttribute("position")]
        [DefaultValue(ModuleTitlePosition.Left)]
        public ModuleTitlePosition Position { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
