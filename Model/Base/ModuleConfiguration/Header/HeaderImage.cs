using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace FomodModel.Base.ModuleConfiguration.Header
{
    /// <summary>
    /// An image.
    /// </summary>
    [Serializable]
    [AspectInjector.Broker.Aspect(typeof(FomodInfrastructure.Aspect.Aspect_INotifyPropertyChanged))]

    public class HeaderImage
    {
        public HeaderImage()
        {
            ShowImage = true;
            ShowFade = true;
            Height = -1;
        }
        
        /// <summary>
        /// The path to the image in the FOMod. If omitted the FOMod's screenshot is used.
        /// </summary>
        [XmlAttribute("path")]
        public string Path { get; set; }

        /// <summary>
        /// Whether or not the image should be displayed.
        /// </summary>
        [XmlAttribute("showImage")]
        [DefaultValue(true)]
        public bool ShowImage { get; set; }

        /// <summary>
        /// Whether or not the fade effect should be displayed. This value is ignored if showImage is false.
        /// </summary>
        [XmlAttribute("showFade")]
        [DefaultValue(true)]
        public bool ShowFade { get; set; }

        /// <summary>
        /// The height to use for the image. Note that there is a minimum height that is enforced based on the user's settings.
        /// </summary>
        [XmlAttribute("height")]
        [DefaultValue(-1)]
        public int Height { get; set; }
    }
}