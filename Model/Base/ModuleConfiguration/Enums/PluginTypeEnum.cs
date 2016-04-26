using System;

namespace FOMOD_Creator.Models.ModuleConfiguration.Enums
{
    /// <summary>
    /// The possible plugin types.
    /// </summary>
    [Serializable]
    public enum PluginTypeEnum
    {
        /// <summary>
        /// Indicates the plugin must be installed.
        /// </summary>
        Required,
        /// <summary>
        /// Indicates the plugin is optional.
        /// </summary>
        Optional,
        /// <summary>
        /// Indicates the plugin is recommended for stability.
        /// </summary>
        Recommended,
        /// <summary>
        /// Indicates that using the plugin could result in instability (i.e., a prerequisite plugin is missing).
        /// </summary>
        NotUsable,
        /// <summary>
        /// Indicates that using the plugin could result in instability if loaded with the currently active plugins (i.e., a prerequisite plugin is missing), but that the prerequisite plugin is installed, just not activated.
        /// </summary>
        CouldBeUsable
    }
}