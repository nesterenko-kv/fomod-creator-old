namespace FOMOD.Creator.Resources.UserControls
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using FOMOD.Creator.Commands;
    using FOMOD.Creator.Domain.Models.ModuleCofiguration;

    public partial class ConditionalFileInstallsUserControl
    {
        public static readonly DependencyProperty FileInstallListProperty = DependencyProperty.Register("FileInstallList", typeof(ConditionalFileInstallList), typeof(ConditionalFileInstallsUserControl), new FrameworkPropertyMetadata
        {
            DefaultValue = null,
            BindsTwoWayByDefault = true,
            PropertyChangedCallback = FileInstallListChange
        });

        public static readonly DependencyProperty SelectedPatternProperty = DependencyProperty.Register("SelectedPattern", typeof(object), typeof(ConditionalFileInstallsUserControl), new FrameworkPropertyMetadata
        {
            DefaultValue = null,
            BindsTwoWayByDefault = true
        });

        private ICommand _addPatternCommand;

        private ICommand _createConditionalFileCommand;

        private ICommand _refreshItemsCommand;

        private ICommand _removeConditionalFileCommand;

        private ICommand _removePatternCommand;

        public ConditionalFileInstallsUserControl()
        {
            InitializeComponent();
            SelectedPattern = FileInstallList?.Patterns?.FirstOrDefault();
        }

        public ICommand AddPatternCommand
        {
            get
            {
                return _addPatternCommand ?? (_addPatternCommand = new RelayCommand(AddPattern));
            }
        }

        public ICommand CreateConditionalFileCommand
        {
            get
            {
                return _createConditionalFileCommand ?? (_createConditionalFileCommand = new RelayCommand(CreateConditionalFile));
            }
        }

        public ConditionalFileInstallList FileInstallList
        {
            get
            {
                return (ConditionalFileInstallList) GetValue(FileInstallListProperty);
            }
            set
            {
                SetValue(FileInstallListProperty, value);
            }
        }

        public ICommand RefreshItemsCommand
        {
            get
            {
                return _refreshItemsCommand ?? (_refreshItemsCommand = new RelayCommand<ItemsControl>(ic => ic.Items.Refresh()));
            }
        }

        public ICommand RemoveConditionalFileCommand
        {
            get
            {
                return _removeConditionalFileCommand ?? (_removeConditionalFileCommand = new RelayCommand(RemoveConditionalFile));
            }
        }

        public ICommand RemovePatternCommand
        {
            get
            {
                return _removePatternCommand ?? (_removePatternCommand = new RelayCommand<ConditionalInstallPattern>(RemovePattern));
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

        private static void FileInstallListChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var s = d as ConditionalFileInstallsUserControl;
            if (s != null)
                s.SelectedPattern = s.FileInstallList?.Patterns?.FirstOrDefault();
        }

        private void AddPattern()
        {
            if (FileInstallList == null)
                return;
            if (FileInstallList.Patterns == null)
                FileInstallList.Patterns = new ObservableCollection<ConditionalInstallPattern>();
            FileInstallList.Patterns.Add(ConditionalInstallPattern.Create());
        }

        private void CreateConditionalFile()
        {
            if (FileInstallList != null)
                return;
            var pattern = ConditionalInstallPattern.Create();
            FileInstallList = ConditionalFileInstallList.Create();
            FileInstallList.Patterns.Add(pattern);
            SelectedPattern = pattern;
        }

        private void RemoveConditionalFile()
        {
            if (FileInstallList != null)
                FileInstallList = null;
        }

        private void RemovePattern(ConditionalInstallPattern param)
        {
            if (FileInstallList?.Patterns == null)
                return;
            FileInstallList.Patterns.Remove(param);
            if (FileInstallList.Patterns.Count == 0)
                FileInstallList.Patterns = null;
        }
    }
}
