using System;

namespace FOMOD_Creator.Models.ModuleConfiguration.Enums
{
    /// <summary>
    /// The possible orders of items.
    /// </summary>
    [Serializable]
    public enum OrderEnum
    {
        /// <summary>
        /// Indicates the items are to be ordered ascending alphabetically.
        /// </summary>
        Ascending,
        
        /// <summary>
        /// Indicates the items are to be ordered descending alphabetically.
        /// </summary>
        Descending,
       
        /// <summary>
        /// Indicates the items are to be ordered as listed in the configuration file.
        /// </summary>
        Explicit
    }
}