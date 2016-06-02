using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;

namespace Module.Editor.Resources.UserControls
{
    public partial class DependencyPatternsUserControl
    {
        public DependencyPatternsUserControl()
        {
            InitializeComponent();
        }

        #region Properties

        public static readonly DependencyProperty PatternsProperty = DependencyProperty.Register("Patterns", typeof(ObservableCollection<DependencyPattern>), typeof(DependencyPatternsUserControl), new FrameworkPropertyMetadata { DefaultValue = null, BindsTwoWayByDefault = true });

        public ObservableCollection<DependencyPattern> Patterns
        {
            get { return (ObservableCollection<DependencyPattern>)GetValue(PatternsProperty); }
            set { SetValue(PatternsProperty, value); }
        }

        #endregion

        #region Methods

        private void AddDependency()
        {
            if (Patterns == null)
                Patterns = new ObservableCollection<DependencyPattern>();
            Patterns.Add(DependencyPattern.Create());
        }

        private void RemoveDependency(DependencyPattern param)
        {
            if (Patterns == null)
                return;
            Patterns.Remove(param);
            if (Patterns.Count == 0)
                Patterns = null;
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
            get { return _removeDependencyCommand ?? (_removeDependencyCommand = new RelayCommand<DependencyPattern>(RemoveDependency)); }
        }

        #endregion
    }
}