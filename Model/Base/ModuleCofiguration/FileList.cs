using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     A list of files and folders.
    /// </summary>
    [Aspect(typeof (AspectINotifyPropertyChanged))]
    [Serializable]
    public class FileList
    {
        [XmlElement("file", typeof (FileSystemItem))]
        public ObservableCollection<FileSystemItem> Files { get; set; }

        [XmlElement("folder", typeof (FileSystemItem))]
        public ObservableCollection<FileSystemItem> Folders { get; set; }
    }
}