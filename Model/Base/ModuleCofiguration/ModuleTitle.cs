using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodModel.Base.ModuleCofiguration.Enum;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     Describes the display properties of the module title.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    [Serializable]
    public class ModuleTitle
    {
        /// <summary>
        ///     ModuleTitle class constructor
        /// </summary>
        public ModuleTitle()
        {
            Position = ModuleTitlePosition.Left;
            ColourXmlSurrogate = "000000";
        }

        /// <summary>
        ///     The identifying name of the condition flag.
        /// </summary>
        [XmlAttribute("position")]
        [DefaultValue(ModuleTitlePosition.Left)]
        public ModuleTitlePosition Position { get; set; }

        /// <summary>
        ///     The colour to use for the title."hexBinary"
        /// </summary>
        [XmlIgnore]
        public Color Colour { get; set; }
        
        [XmlAttribute("colour")]
        [DefaultValue("000000")]
        public string ColourXmlSurrogate
        {
            get { return $"{Colour.ToArgb() & 0x00ffffff:X6}"; }
            set
            {
                uint number;
                Colour = uint.TryParse(value, NumberStyles.HexNumber, null, out number) ? Color.FromArgb((int) (number | 0xff000000)) : Color.Black;
            }
        }

        [XmlText]
        public string Value { get; set; }
    }
}