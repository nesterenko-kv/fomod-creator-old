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
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    [Serializable]
    public class FileList
    {

        public FileList()
        {
            Items.CollectionChanged += Items_CollectionChanged;
        }

        [XmlElement("file", typeof(FileSystemItem))]
        [XmlElement("folder", typeof(FolderSystemItem))]
        public ObservableCollection<SystemItem> Items { get; set; } = new ObservableCollection<SystemItem>();


        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != System.Collections.Specialized.NotifyCollectionChangedAction.Add) return;
            foreach (var item in e.NewItems)
                ((dynamic)item).Parent = Items;
        }
    }
}