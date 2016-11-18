namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;
    using PropertyChanged;

    /// <summary>
    ///     A list of files and folders.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class FileList
    {
        [XmlElement("file", typeof(FileSystemItem))]
        [XmlElement("folder", typeof(FolderSystemItem))]
        public ObservableCollection<SystemItem> Items { get; set; }

        public static FileList Create()
        {
            return new FileList
            {
                Items = new ObservableCollection<SystemItem>()
            };
        }
    }
}
