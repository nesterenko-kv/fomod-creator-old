namespace FOMOD.Creator.Domain.Enums
{
    using System;

    /// <summary>
    ///     <para>
    ///         The possible
    ///         <see cref="FOMOD.Creator.Domain.Models.ModuleCofiguration.Plugin" />
    ///     </para>
    ///     <para>types.</para>
    /// </summary>
    [Serializable]
    public enum PluginTypeEnum
    {
        /// <summary>
        ///     <para>
        ///         Indicates the
        ///         <see cref="FOMOD.Creator.Domain.Models.ModuleCofiguration.Plugin" />
        ///     </para>
        ///     <para>must be installed.</para>
        /// </summary>
        Required,

        /// <summary>
        ///     <para>
        ///         Indicates the
        ///         <see cref="FOMOD.Creator.Domain.Models.ModuleCofiguration.Plugin" />
        ///     </para>
        ///     <para>is optional.</para>
        /// </summary>
        Optional,

        /// <summary>
        ///     <para>
        ///         Indicates the
        ///         <see cref="FOMOD.Creator.Domain.Models.ModuleCofiguration.Plugin" />
        ///     </para>
        ///     <para>is recommended for stability.</para>
        /// </summary>
        Recommended,

        /// <summary>
        ///     <para>
        ///         Indicates that using the
        ///         <see cref="FOMOD.Creator.Domain.Models.ModuleCofiguration.Plugin" />
        ///     </para>
        ///     <para>
        ///         could result in instability (i.e., a prerequisite
        ///         <see cref="FOMOD.Creator.Domain.Models.ModuleCofiguration.Plugin" />
        ///     </para>
        ///     <para>is missing).</para>
        /// </summary>
        NotUsable,

        /// <summary>
        ///     <para>
        ///         Indicates that using the
        ///         <see cref="FOMOD.Creator.Domain.Models.ModuleCofiguration.Plugin" />
        ///     </para>
        ///     <para>
        ///         could result in instability if loaded with the currently active
        ///         plugins (i.e., a prerequisite
        ///         <see cref="FOMOD.Creator.Domain.Models.ModuleCofiguration.Plugin" />
        ///     </para>
        ///     <para>
        ///         is missing), but that the prerequisite
        ///         <see cref="FOMOD.Creator.Domain.Models.ModuleCofiguration.Plugin" />
        ///     </para>
        ///     <para>is installed, just not activated.</para>
        /// </summary>
        CouldBeUsable
    }
}
