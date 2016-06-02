using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base;
using FomodModel.Base.ModuleCofiguration;
using System.IO;
using System.Windows.Input;
using FomodInfrastructure.Interface;
using MahApps.Metro.Controls.Dialogs;
using System.Linq;
using System.Collections.ObjectModel;

namespace Module.Editor.ViewModel
{
    public class ProjectRootViewModel : BaseViewModel<ProjectRoot>
    {
        #region Services

        private readonly IFileBrowserDialog _fileBrowserDialog;
        private readonly IFolderBrowserDialog _folderBrowserDialog;
        private readonly IDialogCoordinator _dialogCoordinator;

        #endregion

        #region Commands

        private ICommand _addImageCommand;
        private ICommand _removeImageCommand;
        private ICommand _browseImageCommand;
        private ICommand _addFileCommand;
        private ICommand _addFolderCommand;

        public ICommand AddImageCommand
        {
            get
            {
                return _addImageCommand ?? (_addImageCommand = new RelayCommand(() =>
                {
                    var imagePath = GetImage();
                    if (!string.IsNullOrEmpty(imagePath))
                        Data.ModuleConfiguration.ModuleImage = HeaderImage.Create(imagePath);
                }));
            }
        }
        
        public ICommand RemoveImageCommand
        {
            get
            {
                return _removeImageCommand ?? (_removeImageCommand = new RelayCommand(() =>
                {
                    Data.ModuleConfiguration.ModuleImage = null;
                }));
            }
        }
        
        public ICommand BrowseImageCommand
        {
            get
            {
                return _browseImageCommand ?? (_browseImageCommand = new RelayCommand(() =>
                {
                    var imagePath = GetImage();
                    if (!string.IsNullOrEmpty(imagePath))
                        Data.ModuleConfiguration.ModuleImage.Path = imagePath;
                }));
            }
        }
        
        public ICommand AddFileCommand
        {
            get
            {
                return _addFileCommand ?? (_addFileCommand = new RelayCommand<string[]>(paths => 
                AddFile(Data.ModuleConfiguration.RequiredInstallFiles.Items, paths)));
            }
        }

        public ICommand AddFolderCommand
        {
            get
            {
                return _addFolderCommand ?? (_addFolderCommand = new RelayCommand<string[]>(paths => 
                AddFolders(Data.ModuleConfiguration.RequiredInstallFiles.Items, paths)));
            }
        }



        private async void AddFile(ObservableCollection<SystemItem> itemSource, string[] paths)
        {
            string[] filesMassive = null;
            if (paths != null)
            {
                filesMassive = paths;
            }
            else
            {
                _fileBrowserDialog.Multiselect = true;
                _fileBrowserDialog.StartFolder = Data.FolderPath;
                _fileBrowserDialog.ShowDialog();
                if (_fileBrowserDialog.SelectedPaths != null)
                    filesMassive = _fileBrowserDialog.SelectedPaths;
                _fileBrowserDialog.Reset();
            }

            if (filesMassive != null)
            {
                if (filesMassive.Where(i => i.StartsWith(Data.FolderPath)).Count() != filesMassive.Count())
                    await _dialogCoordinator.ShowMessageAsync(this, "Attention", "Допускается добавлять файлы только из директории проекта");
                else
                    foreach (var file in filesMassive)
                    {
                        var source = file.Substring(Data.FolderPath.Length);
                        var destination = file.Substring(Data.FolderPath.Length);
                        itemSource.Add(FileSystemItem.Create(source, destination));
                    }
            }
        }
        private async void AddFolders(ObservableCollection<SystemItem> itemSource, string[] paths)
        {
            string[] folderMassive = null;
            if (paths != null)
            {
                folderMassive = paths;
            }
            else
            {
                _folderBrowserDialog.ShowDialog();
                if (!string.IsNullOrWhiteSpace(_folderBrowserDialog.SelectedPath))
                    folderMassive = new string[] { _folderBrowserDialog.SelectedPath };
                _folderBrowserDialog.Reset();
            }

            if (folderMassive != null)
            {
                if (folderMassive.Where(i => i.StartsWith(Data.FolderPath)).Count() != folderMassive.Count())
                    await _dialogCoordinator.ShowMessageAsync(this, "Attention", "Допускается добавлять папки только из директории проекта");
                else
                    foreach (var folder in folderMassive)
                    {
                        var directory = folder.Substring(Data.FolderPath.Length);
                        var destination = folder.Substring(Data.FolderPath.Length);
                        itemSource.Add(FolderSystemItem.Create(directory, destination));
                    }
            }
        }

        #endregion

        public ProjectRootViewModel(IFileBrowserDialog fileBrowserDialog, IFolderBrowserDialog folderBrowserDialog, IDialogCoordinator dialogCoordinator)
        {
            _fileBrowserDialog = fileBrowserDialog;
            _folderBrowserDialog = folderBrowserDialog;
            _dialogCoordinator = dialogCoordinator;
        }
        
        #region Methods

        private string GetImage()
        {
            _fileBrowserDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            _fileBrowserDialog.ShowDialog();
            var selectedPath = _fileBrowserDialog.SelectedPath;
            if (!File.Exists(selectedPath))
                return string.Empty;
            _fileBrowserDialog.Reset();

            var projectPath = FolderPath + Path.DirectorySeparatorChar;
            string relativePath;
            if (!selectedPath.StartsWith(projectPath))
            {
                var directoryName = Path.Combine(projectPath, "Image");
                Directory.CreateDirectory(directoryName);
                var fileNameOnly = Path.GetFileNameWithoutExtension(selectedPath);
                var extension = Path.GetExtension(selectedPath);
                var newPath = Path.Combine(directoryName, fileNameOnly + extension);
                var count = 1;
                while (File.Exists(newPath))
                    newPath = Path.Combine(directoryName, $"{fileNameOnly}({count++})" + extension);
                File.Copy(selectedPath, newPath);
                relativePath = @"Image\" + Path.GetFileName(newPath);
            }
            else
                relativePath = selectedPath.Substring(projectPath.Length);
            return relativePath;
        }

        #endregion
    }
}