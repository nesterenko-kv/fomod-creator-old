using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using FomodModel.Base.ModuleConfiguration.Enums;

namespace FomodModel.Base.ModuleConfiguration.Dependencies
{
    /// <summary>
    /// A specific pattern of mod files and condition flags against which to match the user's installation.
    /// </summary>
    [Serializable]
    public class CompositeDependency
    {
        public CompositeDependency()
        {
            Operator = CompositeDependencyOperator.And;
        }

        [XmlElement("dependencies", typeof(CompositeDependency), Form = XmlSchemaForm.Unqualified)]
        [XmlElement("fileDependency", typeof(FileDependency), Form = XmlSchemaForm.Unqualified)]
        [XmlElement("flagDependency", typeof(FlagDependency), Form = XmlSchemaForm.Unqualified)]
        [XmlElement("fommDependency", typeof(VersionDependency), Form = XmlSchemaForm.Unqualified)]
        [XmlElement("gameDependency", typeof(VersionDependency), Form = XmlSchemaForm.Unqualified)]
        [XmlChoiceIdentifier("ItemsElementName")]
        public object[] Items { get; set; }

        [XmlElement("ItemsElementName")]
        [XmlIgnore]
        public ItemsChoiceType[] ItemsElementName { get; set; }

        /// <summary>
        /// The relation of the contained dependencies.
        /// </summary>
        [XmlAttribute("operator")]
        [DefaultValue(CompositeDependencyOperator.And)]
        public CompositeDependencyOperator Operator { get; set; }
    }
}