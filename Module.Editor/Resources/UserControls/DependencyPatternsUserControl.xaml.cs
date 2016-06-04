using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

        public static readonly DependencyProperty PatternsProperty = DependencyProperty.Register("Patterns", typeof(ObservableCollection<DependencyPattern>), typeof(DependencyPatternsUserControl), new FrameworkPropertyMetadata { DefaultValue = null, BindsTwoWayByDefault = true, PropertyChangedCallback = PattrensListAdd });

        private static void PattrensListAdd(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uc = d as DependencyPatternsUserControl;
            if (uc == null)
                return;
            var firstItem = (e.NewValue as ObservableCollection<DependencyPattern>)?.FirstOrDefault();
            if (firstItem == null && e.NewValue != null)
            {
                firstItem = DependencyPattern.Create();

                {
                    uc.Patterns.Add(firstItem);
                    uc.SelectedPattern = firstItem;
                }
            }
            else
                uc.SelectedPattern = firstItem;
        }

        public static readonly DependencyProperty SelectedPatternProperty = DependencyProperty.Register("SelectedPattern", typeof(object), typeof(DependencyPatternsUserControl), new FrameworkPropertyMetadata { DefaultValue = null, BindsTwoWayByDefault = true });

        public ObservableCollection<DependencyPattern> Patterns
        {
            get { return (ObservableCollection<DependencyPattern>)GetValue(PatternsProperty); }
            set { SetValue(PatternsProperty, value); }
        }

        public object SelectedPattern
        {
            get { return GetValue(SelectedPatternProperty); }
            set { SetValue(SelectedPatternProperty, value); }
        }

        #endregion

        #region Methods

        private void AddPattern()
        {
            if (Patterns == null)
                Patterns = new ObservableCollection<DependencyPattern>();
            else
                Patterns.Add(DependencyPattern.Create());
        }

        private void RemovePattern(DependencyPattern param)
        {
            if (Patterns == null)
                return;
            Patterns.Remove(param);
            if (Patterns.Count == 0)
                Patterns = null;
        }

        #endregion

        #region Commands

        private ICommand _addPatternCommand;

        public ICommand AddPatternCommand
        {
            get { return _addPatternCommand ?? (_addPatternCommand = new RelayCommand(AddPattern)); }
        }

        private ICommand _removePatternCommand;

        public ICommand RemovePatternCommand
        {
            get { return _removePatternCommand ?? (_removePatternCommand = new RelayCommand<DependencyPattern>(RemovePattern)); }
        }

        private ICommand _refreshItemsCommand;

        public ICommand RefreshItemsCommand
        {
            get { return _refreshItemsCommand ?? (_refreshItemsCommand = new RelayCommand<ItemsControl>(ic => ic.Items.Refresh())); }
        }

        #endregion
    }
}