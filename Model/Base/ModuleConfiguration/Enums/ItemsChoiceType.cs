using System;
using System.Xml.Serialization;

namespace FOMOD_Creator.Models.ModuleConfiguration.Enums
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