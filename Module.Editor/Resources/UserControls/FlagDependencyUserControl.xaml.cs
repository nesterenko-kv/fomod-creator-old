using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Module.Editor.Resources.UserControls
{
    public partial class FlagDependencyUserControl
    {
        public FlagDependencyUserControl()
        {
            InitializeComponent();
        }

        public ObservableCollection<FlagDependency> FlagDependencies
        {
            get { return (ObservableCollection<FlagDependency>)GetValue(FlagDependenciesProperty); }
            set { SetValue(FlagDependenciesProperty, value); }
        }

        public static readonly DependencyProperty FlagDependenciesProperty =
            DependencyProperty.Register("FlagDependencies", 
                typeof(ObservableCollection<FlagDependency>), 
                typeof(FlagDependencyUserControl), 
                new FrameworkPropertyMetadata { BindsTwoWayByDefault = true, DefaultValue = null });

        private ICommand _addDependencyCommand;
        public ICommand AddDependencyCommand
        {
            get
            {
                return _addDependencyCommand ?? (_addDependencyCommand = new RelayCommand(() =>
                {
                    if (FlagDependencies == null)
                        FlagDependencies = new ObservableCollection<FlagDependency>();
                    FlagDependencies.Add(FlagDependency.Create());
                }));
            }
        }

        private ICommand _removeDependencyCommand;
        public ICommand RemoveDependencyCommand
        {
            get
            {
                return _removeDependencyCommand ?? (_removeDependencyCommand = new RelayCommand<FlagDependency>(param =>
                {
                    if (FlagDependencies == null) return;
                    FlagDependencies.Remove(param);
                    if (FlagDependencies.Count == 0)
                        FlagDependencies = null;
                }));
            }
        }
    }
}
