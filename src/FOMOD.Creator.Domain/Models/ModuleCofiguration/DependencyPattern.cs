namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.Xml.Serialization;
    using FOMOD.Creator.Domain.Enums;
    using PropertyChanged;

    /// <summary>
    ///     A pattern of mod files and condition flags that determine the type of a
    ///     Plugin.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class DependencyPattern
    {
        /// <summary>
        ///     The list of mods and their states against which to match the user's
        ///     installation.
        /// </summary>
        [XmlElement("dependencies")]
        public CompositeDependency Dependencies { get; set; }

        /// <summary>
        ///     The type of the Plugin.
        /// </summary>
        [XmlElement("type")]
        public PluginType Type { get; set; }

        public static DependencyPattern Create()
        {
            return new DependencyPattern
            {
                Type = new PluginType
                {
                    Name = PluginTypeEnum.NotUsable
                }
            };
        }
    }
}
