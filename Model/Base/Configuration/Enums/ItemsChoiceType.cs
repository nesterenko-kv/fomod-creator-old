using System;
using System.Xml.Serialization;

namespace FomodModel.Base.Configuration.Enums
{
    [Serializable]
    [XmlType(IncludeInSchema = false)]
    public enum ItemsChoiceType
    {
        [XmlEnum("dependencies")]
        Dependencies,
        [XmlEnum("fileDependency")]
        FileDependency,
        [XmlEnum("flagDependency")]
        FlagDependency,
        [XmlEnum("fommDependency")]
        FommDependency,
        [XmlEnum("gameDependency")]
        GameDependency
    }
}