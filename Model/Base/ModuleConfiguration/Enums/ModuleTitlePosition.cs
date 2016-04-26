using System;
using System.Xml.Serialization;

namespace FOMOD_Creator.Models.ModuleConfiguration.Enums
{
    /// <summary>
    /// The possible title positions.
    /// </summary>
    [Serializable]
    [XmlType("moduleTitlePosition", AnonymousType = true)]
    public enum ModuleTitlePosition
    {
        /// <summary>
        /// Positions the title on the left side of the form header.
        /// </summary>
        Left,

        /// <summary>
        /// Positions the title on the right side of the form header.
        /// </summary>
        Right,

        /// <summary>
        /// Positions the title on the right side of the image in the form header.
        /// </summary>
        RightOfImage
    }
}