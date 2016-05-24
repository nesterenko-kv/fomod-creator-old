using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Module.Editor.Resources.UserControls
{
    /// <summary>
    /// Логика взаимодействия для FlagDependencyUserControl.xaml
    /// </summary>
    public partial class FlagDependencyUserControl : UserControl
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


        ICommand _addDependencyCommand; public ICommand AddDependencyCommand
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

        ICommand _removeDependencyCommand; public ICommand RemoveDependencyCommand
        {
            get
            {
                return _removeDependencyCommand ?? (_removeDependencyCommand = new RelayCommand<FlagDependency>(param =>
                {
                    if (FlagDependencies != null)
                        FlagDependencies.Remove(param);
                    if (FlagDependencies.Count == 0) FlagDependencies = null;
                }));
            }
        }
    }
}
