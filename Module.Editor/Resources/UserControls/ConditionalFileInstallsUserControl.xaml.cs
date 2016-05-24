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
    /// Логика взаимодействия для ConditionalFileInstallsUserControl.xaml
    /// </summary>
    public partial class ConditionalFileInstallsUserControl : UserControl
    {
        public ConditionalFileInstallsUserControl()
        {
            InitializeComponent();
        }




        public ConditionalFileInstallList FileInstallList
        {
            get { return (ConditionalFileInstallList)GetValue(FileInstallListProperty); }
            set { SetValue(FileInstallListProperty, value); }
        }

        public static readonly DependencyProperty FileInstallListProperty =
            DependencyProperty.Register("FileInstallList", typeof(ConditionalFileInstallList), typeof(ConditionalFileInstallsUserControl), new FrameworkPropertyMetadata
            {
                DefaultValue = null,
                BindsTwoWayByDefault = true
            });



        ICommand _createConditionalFileInstalls; public ICommand CreateConditionalFileInstalls
        {
            get
            {
                return _createConditionalFileInstalls ?? (_createConditionalFileInstalls = new RelayCommand(() =>
                {
                    if (FileInstallList == null)
                        FileInstallList = ConditionalFileInstallList.Create();
                }));
            }
        }

        ICommand _removeConditionalFileInstalls; public ICommand RemoveConditionalFileInstalls
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

        ICommand _addPatern; public ICommand AddPaternCommand
        {
            get
            {
                return _addPatern ?? (_addPatern = new RelayCommand(() =>
                {
                    if (FileInstallList != null)
                    {
                        if (FileInstallList.Patterns == null)
                            FileInstallList.Patterns = new ObservableCollection<ConditionalInstallPattern>();
                        FileInstallList?.Patterns.Add(ConditionalInstallPattern.Create());
                    }
                }));
            }
        }

        ICommand _removePatern; public ICommand RemovePaternCommand
        {
            get
            {
                return _removePatern ?? (_removePatern = new RelayCommand<ConditionalInstallPattern>(param =>
                {
                    if (FileInstallList != null && FileInstallList.Patterns != null)
                    {
                        FileInstallList.Patterns.Remove(param);
                        if (FileInstallList.Patterns.Count == 0)
                            FileInstallList.Patterns = null;
                    }
                }));
            }
        }
    }

}

