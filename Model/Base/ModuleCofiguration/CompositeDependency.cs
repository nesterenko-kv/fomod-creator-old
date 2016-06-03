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
    [Aspect(typeof(AspectINotifyPropertyChanged)), Serializable]
    public class CompositeDependency
    {
        #region Properties

        /// <summary>
        ///     The relation of the contained dependencies.
        /// </summary>
        [XmlAttribute("operator"), DefaultValue(CompositeDependencyOperator.And)]
        public CompositeDependencyOperator Operator { get; set; }

        [XmlElement("fileDependency", typeof(FileDependency))]
        public ObservableCollection<FileDependency> FileDependencies { get; set; }

        [XmlElement("flagDependency", typeof(FlagDependency))]
        public ObservableCollection<FlagDependency> FlagDependencies { get; set; }

        [XmlElement("gameDependency", typeof(VersionDependency))]
        public VersionDependency GameVersionDependencies { get; set; }

        [XmlElement("fommDependency", typeof(VersionDependency))]
        public VersionDependency FommVersionDependencies { get; set; }

        private CompositeDependency _dependencies;

        [XmlElement("dependencies", typeof(CompositeDependency))]
        public CompositeDependency Dependencies
        {
            get { return _dependencies; }
            set
            {
                _dependencies = value;
                if (value != null)
                    value.Parent = this;
            }
        }

        [XmlIgnore]
        public CompositeDependency Parent { get; set; }

        #endregion

        public CompositeDependency()
        {
            Operator = CompositeDependencyOperator.And;
        }

        public static CompositeDependency Create()
        {
            return new CompositeDependency();
        }
    }
}