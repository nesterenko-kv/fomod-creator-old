using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Controls;
using System.Linq;
using System;

namespace Module.Editor.Resources.UserControls
{
    public partial class ConditionalFileInstallsUserControl
    {

        public static readonly DependencyProperty FileInstallListProperty = DependencyProperty.Register("FileInstallList", typeof(ConditionalFileInstallList), typeof(ConditionalFileInstallsUserControl), new FrameworkPropertyMetadata { DefaultValue = null, BindsTwoWayByDefault = true, PropertyChangedCallback= FileInstallListChange });

        private static void FileInstallListChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var s = d as ConditionalFileInstallsUserControl;
            s.SelectedPattern = s.FileInstallList?.Patterns?.FirstOrDefault();
        }

        public static readonly DependencyProperty SelectedPatternProperty = DependencyProperty.Register("SelectedPattern", typeof(object), typeof(ConditionalFileInstallsUserControl), new FrameworkPropertyMetadata { DefaultValue = null, BindsTwoWayByDefault = true });

        private ICommand _addPatern;

        private ICommand _createConditionalFileInstalls;

        private ICommand _removeConditionalFileInstalls;

        private ICommand _removePatern;

        private ICommand _refreshItemsCommand;


        public ConditionalFileInstallsUserControl()
        {
            InitializeComponent();
            var firstItem = FileInstallList?.Patterns?.FirstOrDefault();
            SelectedPattern = firstItem != null ? firstItem : null;
        }


        public ConditionalFileInstallList FileInstallList
        {
            get { return (ConditionalFileInstallList)GetValue(FileInstallListProperty); }
            set { SetValue(FileInstallListProperty, value); }
        }

        public object SelectedPattern
        {
            get { return (object)GetValue(SelectedPatternProperty); }
            set { SetValue(SelectedPatternProperty, value); }
        }


        public ICommand CreateConditionalFileInstalls
        {
            get
            {
                return _createConditionalFileInstalls ?? (_createConditionalFileInstalls = new RelayCommand(() =>
                {
                    if (FileInstallList == null)
                    {
                        var pattern = ConditionalInstallPattern.Create();
                        FileInstallList = ConditionalFileInstallList.Create();
                        FileInstallList.Patterns.Add(pattern);
                        SelectedPattern = pattern;
                    }
                }));
            }
        }

        public ICommand RemoveConditionalFileInstalls
        {
            get
            {
                return _removeConditionalFileInstalls ?? (_removeConditionalFileInstalls = new RelayCommand(() =>
                {
                    if (FileInstallList != null)
                        FileInstallList = null;
                }));
            }
        }

        public ICommand AddPaternCommand
        {
            get
            {
                return _addPatern ?? (_addPatern = new RelayCommand(() =>
                {
                    if (FileInstallList == null)
                        return;
                    if (FileInstallList.Patterns == null)
                        FileInstallList.Patterns = new ObservableCollection<ConditionalInstallPattern>();
                    FileInstallList?.Patterns.Add(ConditionalInstallPattern.Create());
                }));
            }
        }

        public ICommand RemovePaternCommand
        {
            get
            {
                return _removePatern ?? (_removePatern = new RelayCommand<ConditionalInstallPattern>(param =>
                {
                    if (FileInstallList?.Patterns == null)
                        return;
                    FileInstallList.Patterns.Remove(param);
                    if (FileInstallList.Patterns.Count == 0)
                        FileInstallList.Patterns = null;
                }));
            }
        }


        public ICommand RefreshItemsCommand
        {
            get { return _refreshItemsCommand ?? (_refreshItemsCommand = new RelayCommand<ItemsControl>(ic => ic.Items.Refresh())); }
        }




       


    }
}