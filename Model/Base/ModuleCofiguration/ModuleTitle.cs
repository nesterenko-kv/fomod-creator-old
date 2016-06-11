using System;
using System.ComponentModel;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspects;
using FomodModel.Base.ModuleCofiguration.Enum;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     Describes the display properties of the module title.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged)), Serializable]
    public class ModuleTitle
    {
        #region Properties

        /// <summary>
        ///     The identifying name of the condition flag.
        /// </summary>
        [XmlAttribute("position"), DefaultValue(ModuleTitlePosition.Left)]
        public ModuleTitlePosition Position { get; set; }

        /// <summary>
        ///     The colour to use for the title."hexBinary"
        /// </summary>
        [XmlAttribute("colour"), DefaultValue("000000")]
        public string Colour { get; set; }

        [XmlText]
        public string Value { get; set; }

        #endregion

        public ModuleTitle()
        {
            Position = ModuleTitlePosition.Left;
            Colour = "000000";
        }
    }
}