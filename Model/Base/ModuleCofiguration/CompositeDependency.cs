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
    [Aspect(typeof (AspectINotifyPropertyChanged))]
    [Serializable]
    public class CompositeDependency
    {
        CompositeDependency _dependencies;
        ObservableCollection<FileDependency> _fileDependencies;
        ObservableCollection<FlagDependency> _flagDependencies;

        /// <summary>
        ///     CompositeDependency class constructor
        /// </summary>
        public CompositeDependency()
        {
            Operator = CompositeDependencyOperator.And;
        }

        [XmlElement("dependencies", typeof (CompositeDependency))]
        public CompositeDependency Dependencies { get { return _dependencies; } set { _dependencies = value; if (value != null) value.Parent = this; } }

        [XmlElement("fileDependency", typeof (FileDependency))]
        public ObservableCollection<FileDependency> FileDependencies { get { return _fileDependencies; } set { _fileDependencies = value; ConfigFileDependencies(value); } }


        [XmlElement("flagDependency", typeof (FlagDependency))]
        public ObservableCollection<FlagDependency> FlagDependencies { get { return _flagDependencies; } set { _flagDependencies = value; ConfigFlagDependencies(value); } }

        [XmlElement("fommDependency", typeof (VersionDependency))]
        public VersionDependency FommVersionDependencies { get; set; }

        [XmlElement("gameDependency", typeof (VersionDependency))]
        public VersionDependency GameVersionDependencies { get; set; }

        /// <summary>
        ///     The relation of the contained dependencies.
        /// </summary>
        [XmlAttribute("operator")]
        [DefaultValue(CompositeDependencyOperator.And)]
        public CompositeDependencyOperator Operator { get; set; }

        [XmlIgnore]
        public CompositeDependency Parent { get; set; }


        private void ConfigFileDependencies(ObservableCollection<FileDependency> value)
        {
            if (FileDependencies != null) FileDependencies.CollectionChanged -= Dependencies_CollectionChanged;
            if (value != null) value.CollectionChanged += Dependencies_CollectionChanged;
        }

        private void ConfigFlagDependencies(ObservableCollection<FlagDependency> value)
        {
            if (FlagDependencies != null) FlagDependencies.CollectionChanged -= Dependencies_CollectionChanged; ;
            if (value != null) value.CollectionChanged += Dependencies_CollectionChanged; ;
        }

        private void Dependencies_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                foreach (object item in e.NewItems)
                    (item as dynamic).Parent = this;
        }

        public static CompositeDependency Create()
        {
            return new CompositeDependency
            {

            };
        }
    }
}