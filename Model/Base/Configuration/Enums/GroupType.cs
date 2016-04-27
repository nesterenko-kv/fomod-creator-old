using System;
using System.Xml.Serialization;

namespace FomodModel.Base.Configuration.Enums
{
    /// <summary>
    /// The type of the group.
    /// </summary>
    [Serializable]
    [XmlType("groupType", AnonymousType = true)]
    public enum GroupType
    {
        /// <summary>
        /// At least one plugin in the group must be selected.
        /// </summary>
        SelectAtLeastOne,
        
        /// <summary>
        /// At most one plugin in the group must be selected.
        /// </summary>
        SelectAtMostOne,

        /// <summary>
        /// Exactly one plugin in the group must be selected.
        /// </summary>
        SelectExactlyOne,

        /// <summary>
        /// All plugins in the group must be selected.
        /// </summary>
        SelectAll,

        /// <summary>
        /// Any number of plugins in the group may be selected.
        /// </summary>
        SelectAny
    }
}