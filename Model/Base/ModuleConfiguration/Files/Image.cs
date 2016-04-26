using System;
using System.Xml.Serialization;

namespace FomodModel.Base.ModuleConfiguration.Files
{
    /// <summary>
    /// An image.
    /// </summary>
    [Serializable]
    public class Image
    {
        /// <summary>
        /// The path to the image in the FOMod.
        /// </summary>
        [XmlAttribute("path")]
        public string Path { get; set; }
    }
}