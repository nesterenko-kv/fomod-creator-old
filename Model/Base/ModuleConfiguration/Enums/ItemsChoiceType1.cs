using System;
using System.Xml.Serialization;

namespace FOMOD_Creator.Models.ModuleConfiguration.Enums
{
    [Serializable]
    [XmlType(IncludeInSchema = false)]
    public enum ItemsChoiceType1
    {
        [XmlEnum("file")]
        File,
        [XmlEnum("folder")]
        Folder
    }
}