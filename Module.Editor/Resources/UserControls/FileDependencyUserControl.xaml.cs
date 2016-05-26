using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Module.Editor.Resources.UserControls
{
    /// <summary>
    /// Логика взаимодействия для FileDependencyUserControl.xaml
    /// </summary>
    public partial class FileDependencyUserControl : UserControl
    {
        public FileDependencyUserControl()
        {
            InitializeComponent();
        }


        public ObservableCollection<FileDependency> FileDependencies
        {
            get { return (ObservableCollection<FileDependency>)GetValue(FileDependenciesProperty); }
            set { SetValue(FileDependenciesProperty, value); }
        }

        public static readonly DependencyProperty FileDependenciesProperty =
            DependencyProperty.Register("FileDependencies", typeof(ObservableCollection<FileDependency>), typeof(FileDependencyUserControl), new FrameworkPropertyMetadata { BindsTwoWayByDefault = true, DefaultValue = null});


        ICommand _addDependencyCommand; public ICommand AddDependencyCommand
        {
            get
            {
                return _addDependencyCommand ?? (_addDependencyCommand = new RelayCommand(() =>
                {
                    if (FileDependencies == null)
                        FileDependencies = new ObservableCollection<FileDependency>();
                    FileDependencies.Add(FileDependency.Create("../../file.txt"));
                }));
            }
        }

        ICommand _removeDependencyCommand; public ICommand RemoveDependencyCommand
        {
            get
            {
                return _removeDependencyCommand ?? (_removeDependencyCommand = new RelayCommand<FileDependency>(param =>
                {
                    if (FileDependencies != null)
                        FileDependencies.Remove(param);
                    if (FileDependencies.Count == 0) FileDependencies = null;
                }));
            }
        }

    }
}
