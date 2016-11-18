namespace FOMOD.Creator.Domain.Enums
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    ///     The type of the Group.
    /// </summary>
    [Serializable]
    [XmlType("groupType", AnonymousType = true)]
    public enum GroupType
    {
        /// <summary>
        ///     <para>
        ///         At least one
        ///         <see cref="FOMOD.Creator.Domain.Models.ModuleCofiguration.Plugin" />
        ///     </para>
        ///     <para>
        ///         in the
        ///         <see cref="FOMOD.Creator.Domain.Models.ModuleCofiguration.Group" />
        ///     </para>
        ///     <para>must be selected.</para>
        /// </summary>
        SelectAtLeastOne,

        /// <summary>
        ///     <para>
        ///         At most one
        ///         <see cref="FOMOD.Creator.Domain.Models.ModuleCofiguration.Plugin" />
        ///     </para>
        ///     <para>
        ///         in the
        ///         <see cref="FOMOD.Creator.Domain.Models.ModuleCofiguration.Group" />
        ///     </para>
        ///     <para>must be selected.</para>
        /// </summary>
        SelectAtMostOne,

        /// <summary>
        ///     <para>
        ///         Exactly one
        ///         <see cref="FOMOD.Creator.Domain.Models.ModuleCofiguration.Plugin" />
        ///     </para>
        ///     <para>
        ///         in the
        ///         <see cref="FOMOD.Creator.Domain.Models.ModuleCofiguration.Group" />
        ///     </para>
        ///     <para>must be selected.</para>
        /// </summary>
        SelectExactlyOne,

        /// <summary>
        ///     <para>
        ///         All plugins in the
        ///         <see cref="FOMOD.Creator.Domain.Models.ModuleCofiguration.Group" />
        ///     </para>
        ///     <para>must be selected.</para>
        /// </summary>
        SelectAll,

        /// <summary>
        ///     <para>
        ///         Any number of plugins in the
        ///         <see cref="FOMOD.Creator.Domain.Models.ModuleCofiguration.Group" />
        ///     </para>
        ///     <para>may be selected.</para>
        /// </summary>
        SelectAny
    }
}
