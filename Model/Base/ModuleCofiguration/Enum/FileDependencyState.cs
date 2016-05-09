using System;
using System.Xml.Serialization;

namespace FomodModel.Base.ModuleCofiguration.Enum
{
    /// <summary>
    ///     The possible file state types.
    /// </summary>
    [Serializable]
    [XmlType("fileDependencyState", AnonymousType = true)]
    public enum FileDependencyState
    {
        /// <summary>
        ///     Indicates the mod file is not installed.
        /// </summary>
        Missing,

        /// <summary>
        ///     Indicates the mod file is installed, but not active.
        /// </summary>
        Inactive,

        /// <summary>
        ///     Indicates the mod file is installed and active.
        /// </summary>
        Active
    }
}