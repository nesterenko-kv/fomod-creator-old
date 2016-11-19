namespace FOMOD.Creator.Domain.Enums
{
    using System;
    using System.Xml.Serialization;

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
