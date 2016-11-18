namespace FOMOD.Creator.Resources.UserControls
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;
    using FOMOD.Creator.Commands;
    using FOMOD.Creator.Domain.Models.ModuleCofiguration;

    public partial class FlagDependencyUserControl
    {
        public static readonly DependencyProperty FlagDependenciesProperty = DependencyProperty.Register("FlagDependencies", typeof(ObservableCollection<FlagDependency>), typeof(FlagDependencyUserControl), new FrameworkPropertyMetadata
        {
            BindsTwoWayByDefault = true,
            DefaultValue = null
        });

        private ICommand _addDependencyCommand;

        private ICommand _removeDependencyCommand;

        public FlagDependencyUserControl()
        {
            InitializeComponent();
        }

        public ICommand AddDependencyCommand
        {
            get
            {
                return _addDependencyCommand ?? (_addDependencyCommand = new RelayCommand(AddDependency));
            }
        }

        public ObservableCollection<FlagDependency> FlagDependencies
        {
            get
            {
                return (ObservableCollection<FlagDependency>) GetValue(FlagDependenciesProperty);
            }
            set
            {
                SetValue(FlagDependenciesProperty, value);
            }
        }

        public ICommand RemoveDependencyCommand
        {
            get
            {
                return _removeDependencyCommand ?? (_removeDependencyCommand = new RelayCommand<FlagDependency>(RemoveDependency));
            }
        }

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
    }
}
