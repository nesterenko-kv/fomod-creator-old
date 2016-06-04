using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;

namespace Module.Editor.Resources.UserControls
{
    public partial class ConditionalFileInstallsUserControl
    {
        public ConditionalFileInstallsUserControl()
        {
            InitializeComponent();
            SelectedPattern = FileInstallList?.Patterns?.FirstOrDefault();
        }

        #region Properties

        public static readonly DependencyProperty FileInstallListProperty = DependencyProperty.Register("FileInstallList", typeof(ConditionalFileInstallList), typeof(ConditionalFileInstallsUserControl), new FrameworkPropertyMetadata { DefaultValue = null, BindsTwoWayByDefault = true, PropertyChangedCallback = FileInstallListChange });

        public ConditionalFileInstallList FileInstallList
        {
            get { return (ConditionalFileInstallList)GetValue(FileInstallListProperty); }
            set { SetValue(FileInstallListProperty, value); }
        }

        public static readonly DependencyProperty SelectedPatternProperty = DependencyProperty.Register("SelectedPattern", typeof(object), typeof(ConditionalFileInstallsUserControl), new FrameworkPropertyMetadata { DefaultValue = null, BindsTwoWayByDefault = true });

        public object SelectedPattern
        {
            get { return GetValue(SelectedPatternProperty); }
            set { SetValue(SelectedPatternProperty, value); }
        }

        #endregion

        #region Methods

        private static void FileInstallListChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var s = d as ConditionalFileInstallsUserControl;
            if (s != null)
                s.SelectedPattern = s.FileInstallList?.Patterns?.FirstOrDefault();
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

        private void AddPattern()
        {
            if (FileInstallList == null)
                return;
            if (FileInstallList.Patterns == null)
                FileInstallList.Patterns = new ObservableCollection<ConditionalInstallPattern>();
            FileInstallList.Patterns.Add(ConditionalInstallPattern.Create());
        }

        private void RemovePattern(ConditionalInstallPattern param)
        {
            if (FileInstallList?.Patterns == null)
                return;
            FileInstallList.Patterns.Remove(param);
            if (FileInstallList.Patterns.Count == 0)
                FileInstallList.Patterns = null;
        }

        #endregion

        #region Commands

        private ICommand _createConditionalFileCommand;

        public ICommand CreateConditionalFileCommand
        {
            get { return _createConditionalFileCommand ?? (_createConditionalFileCommand = new RelayCommand(CreateConditionalFile)); }
        }

        private ICommand _removeConditionalFileCommand;

        public ICommand RemoveConditionalFileCommand
        {
            get { return _removeConditionalFileCommand ?? (_removeConditionalFileCommand = new RelayCommand(RemoveConditionalFile)); }
        }

        private ICommand _addPatternCommand;

        public ICommand AddPatternCommand
        {
            get { return _addPatternCommand ?? (_addPatternCommand = new RelayCommand(AddPattern)); }
        }

        private ICommand _removePatternCommand;

        public ICommand RemovePatternCommand
        {
            get { return _removePatternCommand ?? (_removePatternCommand = new RelayCommand<ConditionalInstallPattern>(RemovePattern)); }
        }

        private ICommand _refreshItemsCommand;

        public ICommand RefreshItemsCommand
        {
            get { return _refreshItemsCommand ?? (_refreshItemsCommand = new RelayCommand<ItemsControl>(ic => ic.Items.Refresh())); }
        }

        #endregion
    }
}