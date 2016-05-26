using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Module.Editor.Resources.UserControls
{
    /// <summary>
    /// Логика взаимодействия для DependencyPatternsUserControl.xaml
    /// </summary>
    public partial class DependencyPatternsUserControl : UserControl
    {
        public DependencyPatternsUserControl()
        {
            InitializeComponent();
        }



        public ObservableCollection<DependencyPattern> Patterns
        {
            get { return (ObservableCollection<DependencyPattern>)GetValue(PatternsProperty); }
            set { SetValue(PatternsProperty, value); }
        }

        public static readonly DependencyProperty PatternsProperty =
            DependencyProperty.Register("Patterns", typeof(ObservableCollection<DependencyPattern>), typeof(DependencyPatternsUserControl), 
                new FrameworkPropertyMetadata
                {
                    DefaultValue = null,
                    BindsTwoWayByDefault = true
                });


        ICommand _addDependencyCommand; public ICommand AddDependencyCommand
        {
            get
            {
                return _addDependencyCommand ?? (_addDependencyCommand = new RelayCommand(() =>
                {
                    if (Patterns == null)
                        Patterns = new ObservableCollection<DependencyPattern>();
                    Patterns.Add(DependencyPattern.Create());
                }));
            }
        }

        ICommand _removeDependencyCommand; public ICommand RemoveDependencyCommand
        {
            get
            {
                return _removeDependencyCommand ?? (_removeDependencyCommand = new RelayCommand<DependencyPattern>(param =>
                {
                    if (Patterns != null)
                        Patterns.Remove(param);
                    if (Patterns.Count == 0) Patterns = null;
                }));
            }
        }
    }
}
