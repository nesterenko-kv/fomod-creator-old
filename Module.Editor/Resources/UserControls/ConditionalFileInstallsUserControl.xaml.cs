using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
using Microsoft.Practices.ServiceLocation;

namespace Module.Editor.Resources.UserControls
{
    public partial class ConditionalFileInstallsUserControl
    {
        public static readonly DependencyProperty ServiceLocatorProperty = DependencyProperty.Register("ServiceLocator", typeof(IServiceLocator), typeof(ConditionalFileInstallsUserControl), new PropertyMetadata(null));

        public static readonly DependencyProperty FileInstallListProperty = DependencyProperty.Register("FileInstallList", typeof(ConditionalFileInstallList), typeof(ConditionalFileInstallsUserControl), new FrameworkPropertyMetadata { DefaultValue = null, BindsTwoWayByDefault = true });

        private ICommand _addPatern;

        private ICommand _createConditionalFileInstalls;

        private ICommand _removeConditionalFileInstalls;

        private ICommand _removePatern;

        public ConditionalFileInstallsUserControl()
        {
            InitializeComponent();
        }

        public IServiceLocator ServiceLocator
        {
            get { return (IServiceLocator)GetValue(ServiceLocatorProperty); }
            set { SetValue(ServiceLocatorProperty, value); }
        }

        public ConditionalFileInstallList FileInstallList
        {
            get { return (ConditionalFileInstallList)GetValue(FileInstallListProperty); }
            set { SetValue(FileInstallListProperty, value); }
        }

        public ICommand CreateConditionalFileInstalls
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
    }
}