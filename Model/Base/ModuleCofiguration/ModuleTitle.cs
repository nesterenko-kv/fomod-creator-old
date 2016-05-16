using System;
using System.ComponentModel;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodModel.Base.ModuleCofiguration.Enum;
using System.Runtime.Serialization;
using FomodModel.Base.ModuleCofiguration.Struct;
using System.Diagnostics;
using System.Xml;
using System.Xml.Schema;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     Describes the display properties of the module title.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    [Serializable]
    public class ModuleTitle: IXmlSerializable
    {
        /// <summary>
        ///     ModuleTitle class constructor
        /// </summary>
        public ModuleTitle()
        {
            Position = ModuleTitlePosition.Left;
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
        [XmlAttribute("colour", DataType = "hexBinary")]
        [XmlIgnore]
        public PixelColor? Colour { get; set; } 

        [XmlText]
        public string Value { get; set; }


        #region IXmlSerializable

        public XmlSchema GetSchema() => null;

        public void ReadXml(XmlReader reader)
        {
            string atr = reader.GetAttribute("colour");
            if (atr != null && atr.Length == 6)
            {
                byte red = 0;
                byte blue = 0;
                byte green = 0;
                if (byte.TryParse(atr.Substring(0, 2), out red) && byte.TryParse(atr.Substring(2, 2), out blue) && byte.TryParse(atr.Substring(4, 2), out green))
                    Colour = new PixelColor { Red = red, Green = green, Blue = blue };
                return;
                    
            }
            Colour = null;
        }

        public void WriteXml(XmlWriter writer)
        {
            if (Colour != null)
                writer.WriteAttributeString("colour", Colour.Value.Red + Colour.Value.Blue + Colour.Value.Green + "");
            else
                writer.WriteAttributeString("colour", null);
            
        } 

        #endregion
    }
}