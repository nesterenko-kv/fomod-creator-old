using System;
using System.Xml.Serialization;

namespace FomodModel.Base.ModuleCofiguration.Enum
{
    /// <summary>
    /// The type of the Group.
    /// </summary>
    [Serializable]
    [XmlType("groupType", AnonymousType = true)]
    public enum GroupType
    {
        /// <summary>
        ///     At least one Plugin in the Group must be selected.
        /// </summary>
        SelectAtLeastOne,

        /// <summary>
        ///     At most one Plugin in the Group must be selected.
        /// </summary>
        SelectAtMostOne,

        /// <summary>
        ///     Exactly one Plugin in the Group must be selected.
        /// </summary>
        SelectExactlyOne,

        /// <summary>
        ///     All plugins in the Group must be selected.
        /// </summary>
        SelectAll,

        /// <summary>
        ///     Any number of plugins in the Group may be selected.
        /// </summary>
        SelectAny
    }
}