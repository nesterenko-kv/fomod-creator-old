using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.ServiceLocation;
using System.Collections.ObjectModel;
using System.Linq;
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






        public IServiceLocator ServiceLocator
        {
            get { return (IServiceLocator)GetValue(ServiceLocatorProperty); }
            set { SetValue(ServiceLocatorProperty, value); }
        }

        public static readonly DependencyProperty ServiceLocatorProperty =
            DependencyProperty.Register("ServiceLocator", typeof(IServiceLocator), typeof(ConditionalFileInstallsUserControl), new PropertyMetadata(null));






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





        //IFileBrowserDialog _fileBrowserDialog;
        //IDialogCoordinator _dialogCoordinator;
        //IFolderBrowserDialog _folderBrowserDialog;

        //private async void AddFile(ObservableCollection<SystemItem> itemSource, string[] paths)
        //{
        //    if (ServiceLocator == null) return;
        //    if (_fileBrowserDialog == null) _fileBrowserDialog = ServiceLocator.GetInstance<IFileBrowserDialog>();

        //    string[] filesMassive = null;
        //    if (paths != null)
        //    {
        //        filesMassive = paths;
        //    }
        //    else
        //    {
        //        _fileBrowserDialog.Multiselect = true;
        //        _fileBrowserDialog.StartFolder = Data.FolderPath;
        //        _fileBrowserDialog.ShowDialog();
        //        if (_fileBrowserDialog.SelectedPaths != null)
        //            filesMassive = _fileBrowserDialog.SelectedPaths;
        //        _fileBrowserDialog.Reset();
        //    }

        //    if (filesMassive != null)
        //    {
        //        if (filesMassive.Where(i => i.StartsWith(Data.FolderPath)).Count() != filesMassive.Count())
        //            await _dialogCoordinator.ShowMessageAsync(this, "Attention", "Допускается добавлять файлы только из директории проекта");
        //        else
        //            foreach (var file in filesMassive)
        //            {
        //                var source = file.Substring(Data.FolderPath.Length);
        //                var destination = file.Substring(Data.FolderPath.Length);
        //                itemSource.Add(FileSystemItem.Create(source, destination));
        //            }
        //    }
        //}
        //private async void AddFolders(ObservableCollection<SystemItem> itemSource, string[] paths)
        //{
        //    if (ServiceLocator == null) return;
        //    if (_folderBrowserDialog == null) _folderBrowserDialog = ServiceLocator.GetInstance<IFolderBrowserDialog>();

        //    string[] folderMassive = null;
        //    if (paths != null)
        //    {
        //        folderMassive = paths;
        //    }
        //    else
        //    {
        //        _folderBrowserDialog.ShowDialog();
        //        if (!string.IsNullOrWhiteSpace(_folderBrowserDialog.SelectedPath))
        //            folderMassive = new string[] { _folderBrowserDialog.SelectedPath };
        //        _folderBrowserDialog.Reset();
        //    }

        //    if (folderMassive != null)
        //    {
        //        if (folderMassive.Where(i => i.StartsWith(Data.FolderPath)).Count() != folderMassive.Count())
        //            await _dialogCoordinator.ShowMessageAsync(this, "Attention", "Допускается добавлять папки только из директории проекта");
        //        else
        //            foreach (var folder in folderMassive)
        //            {
        //                var directory = folder.Substring(Data.FolderPath.Length);
        //                var destination = folder.Substring(Data.FolderPath.Length);
        //                itemSource.Add(FolderSystemItem.Create(directory, destination));
        //            }
        //    }
        //}

    }

}

