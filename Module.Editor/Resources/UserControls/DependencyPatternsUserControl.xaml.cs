using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
using System.Windows.Controls;
using System;
using System.Linq;

namespace Module.Editor.Resources.UserControls
{
    public partial class DependencyPatternsUserControl
    {
        public DependencyPatternsUserControl()
        {
            InitializeComponent();
        }




        #region Properties

        public static readonly DependencyProperty PatternsProperty = DependencyProperty.Register("Patterns", typeof(ObservableCollection<DependencyPattern>), typeof(DependencyPatternsUserControl), new FrameworkPropertyMetadata { DefaultValue = null, BindsTwoWayByDefault = true, PropertyChangedCallback=PattrensListAdd });

        private static void PattrensListAdd(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uc = d as DependencyPatternsUserControl;
            var firstItem = (e.NewValue as ObservableCollection<DependencyPattern>)?.FirstOrDefault();
            if (firstItem == null && e.NewValue != null)
            {
                firstItem = DependencyPattern.Create();
                uc.Patterns.Add(firstItem);
                uc.SelectedPattern = firstItem;
            }
            else
            {
                uc.SelectedPattern = firstItem;
            }
        }

        public static readonly DependencyProperty SelectedPatternProperty = DependencyProperty.Register("SelectedPattern", typeof(object), typeof(DependencyPatternsUserControl), new FrameworkPropertyMetadata { DefaultValue = null, BindsTwoWayByDefault = true });


        public ObservableCollection<DependencyPattern> Patterns
        {
            get { return (ObservableCollection<DependencyPattern>)GetValue(PatternsProperty); }
            set { SetValue(PatternsProperty, value); }
        }


        public object SelectedPattern
        {
            get { return (object)GetValue(SelectedPatternProperty); }
            set { SetValue(SelectedPatternProperty, value); }
        }

        #endregion


        #region Commands

        private ICommand _addPatern;

        private ICommand _removePatern;

        private ICommand _refreshItemsCommand;

        public ICommand AddPaternCommand
        {
            get
            {
                return _addPatern ?? (_addPatern = new RelayCommand(() =>
                {
                    if (Patterns == null)
                        Patterns = new ObservableCollection<DependencyPattern>();
                    else
                        Patterns.Add(DependencyPattern.Create());
                }));
            }
        }

        public ICommand RemovePaternCommand
        {
            get
            {
                return _removePatern ?? (_removePatern = new RelayCommand<DependencyPattern>(param =>
                {
                    if (Patterns == null)
                        return;
                    Patterns.Remove(param);
                    if (Patterns.Count == 0)
                        Patterns = null;
                }));
            }
        }


        public ICommand RefreshItemsCommand
        {
            get { return _refreshItemsCommand ?? (_refreshItemsCommand = new RelayCommand<ItemsControl>(ic => ic.Items.Refresh())); }
        }
        #endregion
    }
}