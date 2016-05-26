using System;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     An image.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    [Serializable]
    public class Image
    {
        #region Properties

        /// <summary>
        ///     The path to the image in the FOMod.
        /// </summary>
        [XmlAttribute("path")]
        public string Path { get; set; }

        public static Image Create(string imagePath)
        {
            return new Image
            {
                Path = imagePath
            };
        }

        #endregion
    }
}