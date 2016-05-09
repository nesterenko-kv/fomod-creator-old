using System;

namespace FomodModel.Base.ModuleCofiguration.Enum
{
    /// <summary>
    ///     The possible Plugin types.
    /// </summary>
    [Serializable]
    public enum PluginTypeEnum
    {
        /// <summary>
        ///     Indicates the Plugin must be installed.
        /// </summary>
        Required,

        /// <summary>
        ///     Indicates the Plugin is optional.
        /// </summary>
        Optional,

        /// <summary>
        ///     Indicates the Plugin is recommended for stability.
        /// </summary>
        Recommended,

        /// <summary>
        ///     Indicates that using the Plugin could result in instability (i.e., a prerequisite Plugin is missing).
        /// </summary>
        NotUsable,

        /// <summary>
        ///     Indicates that using the Plugin could result in instability if loaded with the currently active plugins (i.e., a
        ///     prerequisite Plugin is missing), but that the prerequisite Plugin is installed, just not activated.
        /// </summary>
        CouldBeUsable
    }
}