using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
