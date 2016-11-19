namespace FOMOD.Creator.Resources.UserControls
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using FOMOD.Creator.Commands;
    using FOMOD.Creator.Domain.Models.ModuleCofiguration;

    public partial class DependencyPatternsUserControl
    {
        public static readonly DependencyProperty PatternsProperty = DependencyProperty.Register("Patterns", typeof(ObservableCollection<DependencyPattern>), typeof(DependencyPatternsUserControl), new FrameworkPropertyMetadata
        {
            DefaultValue = null,
            BindsTwoWayByDefault = true,
            PropertyChangedCallback = PatternsListAdd
        });

        public static readonly DependencyProperty SelectedPatternProperty = DependencyProperty.Register("SelectedPattern", typeof(object), typeof(DependencyPatternsUserControl), new FrameworkPropertyMetadata
        {
            DefaultValue = null,
            BindsTwoWayByDefault = true
        });

        private ICommand _addPatternCommand;

        private ICommand _refreshItemsCommand;

        private ICommand _removePatternCommand;

        public DependencyPatternsUserControl()
        {
            InitializeComponent();
        }

        public ICommand AddPatternCommand
        {
            get
            {
                return _addPatternCommand ?? (_addPatternCommand = new RelayCommand(AddPattern));
            }
        }

        public ObservableCollection<DependencyPattern> Patterns
        {
            get
            {
                return (ObservableCollection<DependencyPattern>) GetValue(PatternsProperty);
            }
            set
            {
                SetValue(PatternsProperty, value);
            }
        }

        public ICommand RefreshItemsCommand
        {
            get
            {
                return _refreshItemsCommand ?? (_refreshItemsCommand = new RelayCommand<ItemsControl>(ic => ic.Items.Refresh()));
            }
        }

        public ICommand RemovePatternCommand
        {
            get
            {
                return _removePatternCommand ?? (_removePatternCommand = new RelayCommand<DependencyPattern>(RemovePattern));
            }
        }

        public object SelectedPattern
        {
            get
            {
                return GetValue(SelectedPatternProperty);
            }
            set
            {
                SetValue(SelectedPatternProperty, value);
            }
        }

        private static void PatternsListAdd(DependencyObject d, DependencyPropertyChangedEventArgs e)
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
    }
}
