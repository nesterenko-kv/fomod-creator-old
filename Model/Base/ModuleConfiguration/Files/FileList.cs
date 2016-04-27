using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using FomodModel.Base.ModuleConfiguration.Enums;

namespace FomodModel.Base.ModuleConfiguration.Files
{
    /// <summary>
    /// A list of files and folders.
    /// </summary>
    [Serializable]
    [AspectInjector.Broker.Aspect(typeof(FomodInfrastructure.Aspect.AspectINotifyPropertyChanged))]

    public class FileList
    {
        [XmlElement("file", typeof(FileSystemItem), Form = XmlSchemaForm.Unqualified)]
        [XmlElement("folder", typeof(FileSystemItem), Form = XmlSchemaForm.Unqualified)]
        [XmlChoiceIdentifier("ItemsElementName")]
        public FileSystemItem[] Items { get; set; }

        [XmlElement("ItemsElementName")]
        [XmlIgnore]
        public ItemsChoiceType1[] ItemsElementName { get; set; }
    }
}