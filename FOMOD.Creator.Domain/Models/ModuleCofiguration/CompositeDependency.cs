namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using FOMOD.Creator.Domain.Enums;
    using PropertyChanged;

    /// <summary>
    ///     A dependency that is made up of one or more dependencies.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class CompositeDependency
    {
        private CompositeDependency _dependencies;

        public CompositeDependency()
        {
            Operator = CompositeDependencyOperator.And;
        }

        [XmlElement("flagDependency", typeof(FlagDependency))]
        public ObservableCollection<FlagDependency> FlagDependencies { get; set; }

        [XmlElement("gameDependency", typeof(VersionDependency))]
        public VersionDependency GameVersionDependencies { get; set; }

        [XmlElement("fommDependency", typeof(VersionDependency))]
        public VersionDependency FommVersionDependencies { get; set; }

        [XmlElement("dependencies", typeof(CompositeDependency))]
        public CompositeDependency Dependencies
        {
            get
            {
                return _dependencies;
            }
            set
            {
                _dependencies = value;
                if (value != null)
                    value.Parent = this;
            }
        }

        [XmlElement("fileDependency", typeof(FileDependency))]
        public ObservableCollection<FileDependency> FileDependencies { get; set; }


        /// <summary>
        ///     The relation of the contained dependencies.
        /// </summary>
        [XmlAttribute("operator")]
        [DefaultValue(CompositeDependencyOperator.And)]
        public CompositeDependencyOperator Operator { get; set; }

        [XmlIgnore]
        public CompositeDependency Parent { get; set; }

        public static CompositeDependency Create()
        {
            return new CompositeDependency();
        }
    }
}
