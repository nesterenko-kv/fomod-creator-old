using System;
using System.Xml.Serialization;

namespace FomodModel.Base.Configuration.Enums
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