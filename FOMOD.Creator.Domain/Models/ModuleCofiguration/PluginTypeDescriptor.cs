namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.Xml.Serialization;
    using PropertyChanged;

    /// <summary>
    ///     Describes the type of a Plugin.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class PluginTypeDescriptor
    {
        [XmlElement("dependencyType", typeof(DependencyPluginType))]
        public DependencyPluginType DependencyType { get; set; }

        [XmlElement("type", typeof(PluginType))]
        public PluginType Type { get; set; }

        public static PluginTypeDescriptor Create()
        {
            return new PluginTypeDescriptor
            {
                Type = PluginType.Create()
            };
        }
    }
}
