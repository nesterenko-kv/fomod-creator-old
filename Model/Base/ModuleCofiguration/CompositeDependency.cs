using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodModel.Base.ModuleCofiguration.Enum;

namespace FomodModel.Base.ModuleCofiguration
{
    /// <summary>
    ///     A dependency that is made up of one or more dependencies.
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    [Serializable]
    public class CompositeDependency
    {
        /// <summary>
        ///     CompositeDependency class constructor
        /// </summary>
        public CompositeDependency()
        {
            Operator = CompositeDependencyOperator.And;
        }

        [XmlElement("dependencies", typeof(CompositeDependency))]
        public ObservableCollection<CompositeDependency> Dependencies { get; set; }

        [XmlElement("fileDependency", typeof(FileDependency))]
        public ObservableCollection<FileDependency> FileDependencies { get; set; }

        [XmlElement("flagDependency", typeof(FlagDependency))]
        public ObservableCollection<FlagDependency> FlagDependencies { get; set; }

        [XmlElement("fommDependency", typeof(VersionDependency))]
        public VersionDependency FommVersionDependencies { get; set; }

        [XmlElement("gameDependency", typeof(VersionDependency))]
        public VersionDependency GameVersionDependencies { get; set; }

        /// <summary>
        ///     The relation of the contained dependencies.
        /// </summary>
        [XmlAttribute("operator")]
        [DefaultValue(CompositeDependencyOperator.And)]
        public CompositeDependencyOperator Operator { get; set; }
    }
}