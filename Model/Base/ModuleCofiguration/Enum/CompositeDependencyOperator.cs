using System;
using System.Xml.Serialization;

namespace FomodModel.Base.ModuleCofiguration.Enum
{
    /// <summary>
    ///     The relation of the contained dependencies.
    /// </summary>
    [Serializable]
    [XmlType("CompositeDependencyOperator", AnonymousType = true)]
    public enum CompositeDependencyOperator
    {
        /// <summary>
        ///     Indicates all contained dependencies must be satisfied in order for this dependency to be satisfied.
        /// </summary>
        And,

        /// <summary>
        ///     Indicates at least one listed dependency must be satisfied in order for this dependency to be satisfied.
        /// </summary>
        Or
    }
}