using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodModel.Base.Configuration.Enums;

namespace FomodModel.Base.Configuration.Files
{
    /// <summary>
    /// A list of files and folders.
    /// </summary>
    [Serializable]
    [Aspect(typeof(AspectINotifyPropertyChanged))]

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