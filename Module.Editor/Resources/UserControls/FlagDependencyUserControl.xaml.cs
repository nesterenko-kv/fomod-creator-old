using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;

namespace Module.Editor.Resources.UserControls
{
    public partial class FlagDependencyUserControl
    {
        public FlagDependencyUserControl()
        {
            InitializeComponent();
        }

        #region Properties

        public static readonly DependencyProperty FlagDependenciesProperty = DependencyProperty.Register("FlagDependencies", typeof(ObservableCollection<FlagDependency>), typeof(FlagDependencyUserControl), new FrameworkPropertyMetadata { BindsTwoWayByDefault = true, DefaultValue = null });

        public ObservableCollection<FlagDependency> FlagDependencies
        {
            get { return (ObservableCollection<FlagDependency>)GetValue(FlagDependenciesProperty); }
            set { SetValue(FlagDependenciesProperty, value); }
        }

        #endregion

        #region Methods

        private void AddDependency()
        {
            if (FlagDependencies == null)
                FlagDependencies = new ObservableCollection<FlagDependency>();
            FlagDependencies.Add(FlagDependency.Create());
        }

        private void RemoveDependency(FlagDependency param)
        {
            if (FlagDependencies == null)
                return;
            FlagDependencies.Remove(param);
            if (FlagDependencies.Count == 0)
                FlagDependencies = null;
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
            get { return _removeDependencyCommand ?? (_removeDependencyCommand = new RelayCommand<FlagDependency>(RemoveDependency)); }
        }

        #endregion
    }
}