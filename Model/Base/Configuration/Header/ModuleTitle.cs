using System;
using System.ComponentModel;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodModel.Base.Configuration.Enums;

namespace FomodModel.Base.Configuration.Header
{
    /// <summary>
    /// Describes the display properties of the module title.
    /// </summary>
    [Serializable]
    [Aspect(typeof(AspectINotifyPropertyChanged))]

    public class ModuleTitle
    {
        public ModuleTitle()
        {
            Position = ModuleTitlePosition.Left;
        }

        /// <summary>
        /// A possible title positions.
        /// </summary>
        [XmlAttribute("position")]
        [DefaultValue(ModuleTitlePosition.Left)]
        public ModuleTitlePosition Position { get; set; }

        /// <summary>
        /// The colour to use for the title.
        /// </summary>
        [XmlAttribute("colour", DataType = "hexBinary")]
        public byte[] Colour { get; set; }

        /// <summary>
        /// The name of modification.
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }
}