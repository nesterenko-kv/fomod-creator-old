namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.Xml.Serialization;
    using FOMOD.Creator.Domain.Enums;
    using PropertyChanged;

    /// <summary>
    ///     The type of a given Plugin.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class PluginType
    {
        /// <summary>
        ///     The name of the <see cref="Plugin" /> type.
        /// </summary>
        [XmlAttribute("name")]
        public PluginTypeEnum Name { get; set; }

        public static PluginType Create()
        {
            return new PluginType
            {
                Name = PluginTypeEnum.NotUsable
            };
        }
    }
}
