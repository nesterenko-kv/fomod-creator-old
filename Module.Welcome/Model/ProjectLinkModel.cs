using System;
using System.Xml.Serialization;

namespace Module.Welcome.Model
{
    [Serializable]
    public class ProjectLinkModel
    {
        [XmlAttribute]
        public string FolderPath { get; set; }
    }
}
