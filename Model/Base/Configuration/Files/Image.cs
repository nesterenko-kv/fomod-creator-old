using System;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace FomodModel.Base.Configuration.Files
{
    /// <summary>
    /// An image.
    /// </summary>
    [Serializable]
    [Aspect(typeof(AspectINotifyPropertyChanged))]

    public class Image
    {
        /// <summary>
        /// The path to the image in the FOMod.
        /// </summary>
        [XmlAttribute("path")]
        public string Path { get; set; }
    }
}