using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Module.Editor.Resources.UserControls
{
    public partial class ConditionalFileInstallsUserControl
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

        private ICommand _createConditionalFileInstalls;
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

        private ICommand _removeConditionalFileInstalls;
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

        private ICommand _addPatern;
        public ICommand AddPaternCommand
        {
            get
            {
                return _addPatern ?? (_addPatern = new RelayCommand(() =>
                {
                    if (FileInstallList == null) return;
                    if (FileInstallList.Patterns == null)
                        FileInstallList.Patterns = new ObservableCollection<ConditionalInstallPattern>();
                    FileInstallList?.Patterns.Add(ConditionalInstallPattern.Create());
                }));
            }
        }

        private ICommand _removePatern;
        public ICommand RemovePaternCommand
        {
            get
            {
                return _removePatern ?? (_removePatern = new RelayCommand<ConditionalInstallPattern>(param =>
                {
                    if (FileInstallList?.Patterns == null) return;
                    FileInstallList.Patterns.Remove(param);
                    if (FileInstallList.Patterns.Count == 0)
                        FileInstallList.Patterns = null;
                }));
            }
        }
    }

}

