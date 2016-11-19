namespace FOMOD.Creator.Resources.UserControls
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;
    using FOMOD.Creator.Commands;
    using FOMOD.Creator.Domain.Models.ModuleCofiguration;

    public partial class FileDependencyUserControl
    {
        public static readonly DependencyProperty FileDependenciesProperty = DependencyProperty.Register("FileDependencies", typeof(ObservableCollection<FileDependency>), typeof(FileDependencyUserControl), new FrameworkPropertyMetadata
        {
            BindsTwoWayByDefault = true,
            DefaultValue = null
        });

        private ICommand _addDependencyCommand;

        private ICommand _removeDependencyCommand;

        public FileDependencyUserControl()
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

        public ObservableCollection<FileDependency> FileDependencies
        {
            get
            {
                return (ObservableCollection<FileDependency>) GetValue(FileDependenciesProperty);
            }
            set
            {
                SetValue(FileDependenciesProperty, value);
            }
        }

        public ICommand RemoveDependencyCommand
        {
            get
            {
                return _removeDependencyCommand ?? (_removeDependencyCommand = new RelayCommand<FileDependency>(RemoveDependency));
            }
        }

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
    }
}
