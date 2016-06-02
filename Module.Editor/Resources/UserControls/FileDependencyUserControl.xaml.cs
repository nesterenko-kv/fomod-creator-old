using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;

namespace Module.Editor.Resources.UserControls
{
    public partial class FileDependencyUserControl
    {
        public FileDependencyUserControl()
        {
            InitializeComponent();
        }

        #region Properties

        public static readonly DependencyProperty FileDependenciesProperty = DependencyProperty.Register("FileDependencies", typeof(ObservableCollection<FileDependency>), typeof(FileDependencyUserControl), new FrameworkPropertyMetadata { BindsTwoWayByDefault = true, DefaultValue = null });

        public ObservableCollection<FileDependency> FileDependencies
        {
            get { return (ObservableCollection<FileDependency>)GetValue(FileDependenciesProperty); }
            set { SetValue(FileDependenciesProperty, value); }
        }

        #endregion

        #region Methods

        private void AddDependency()
        {
            if (FileDependencies == null)
                FileDependencies = new ObservableCollection<FileDependency>();
            FileDependencies.Add(FileDependency.Create());
        }

        private void RemoveDependency(FileDependency param)
        {
            if (FileDependencies == null)
                return;
            FileDependencies.Remove(param);
            if (FileDependencies.Count == 0)
                FileDependencies = null;
        }

        #endregion

        #region Commands

        private ICommand _addDependencyCommand;

        public ICommand AddDependencyCommand
        {
            get { return _addDependencyCommand ?? (_addDependencyCommand = new RelayCommand(AddDependency)); }
        }

        private ICommand _removeDependencyCommand;

        public ICommand RemoveDependencyCommand
        {
            get { return _removeDependencyCommand ?? (_removeDependencyCommand = new RelayCommand<FileDependency>(RemoveDependency)); }
        }

        #endregion
    }
}