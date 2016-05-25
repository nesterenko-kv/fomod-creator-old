using System;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using System.ComponentModel;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     An image.
    /// </summary>
    [Serializable]
    public class Image : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        #endregion

        #region Properties

        /// <summary>
        ///     The path to the image in the FOMod.
        /// </summary>
        [XmlAttribute("path")]
        string _path; public string Path { get { return _path; } set { _path = value; OnPropertyChanged("Path"); } }

        public static Image Create(string imagePath)
        {
            return new Image { Path = imagePath };
        }

        #endregion
    }
}