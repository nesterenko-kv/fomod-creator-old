namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.Xml.Serialization;
    using PropertyChanged;

    /// <summary>
    ///     An image.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class Image
    {
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
    }
}
