namespace FOMOD.Creator.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using System.Windows.Input;
    using FOMOD.Creator.Commands;
    using FOMOD.Creator.Domain.Models.ModuleCofiguration;
    using FOMOD.Creator.Properties;
    using MahApps.Metro.Controls.Dialogs;

    public abstract class FileWorkerBaseViewModel<T> : BaseViewModel<T> where T : class
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        private ICommand _addFileCommand;
        private ICommand _addFolderCommand;
        private ICommand _addImageCommand;
        private ICommand _browseImageCommand;
        private ICommand _removeImageCommand;

        protected FileWorkerBaseViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;
        }

        public ICommand AddFileCommand
        {
            get
            {
                return _addFileCommand ?? (_addFileCommand = new RelayCommand<List<string>>(AddFile));
            }
        }

        public ICommand AddFolderCommand
        {
            get
            {
                return _addFolderCommand ?? (_addFolderCommand = new RelayCommand<List<string>>(AddFolder));
            }
        }

        public ICommand AddImageCommand
        {
            get
            {
                return _addImageCommand ?? (_addImageCommand = new RelayCommand(AddImage));
            }
        }

        public ICommand BrowseImageCommand
        {
            get
            {
                return _browseImageCommand ?? (_browseImageCommand = new RelayCommand(BrowseImage));
            }
        }

        public ICommand RemoveImageCommand
        {
            get
            {
                return _removeImageCommand ?? (_removeImageCommand = new RelayCommand(RemoveImage));
            }
        }

        protected abstract void AddFile(List<string> obj);

        protected abstract void AddFolder(List<string> obj);

        protected abstract void AddImage();

        protected abstract void BrowseImage();

        protected abstract void RemoveImage();

        protected void TryAddFiles(ObservableCollection<SystemItem> itemSource, List<string> paths)
        {
            if (itemSource == null)
                throw new ArgumentNullException(nameof(itemSource));
            if (paths == null)
            {
                var fileBrowserDialog = new OpenFileDialog
                {
                    Multiselect = true,
                    CheckFileExists = true,
                    InitialDirectory = FolderPath
                };
                if (fileBrowserDialog.ShowDialog() == DialogResult.OK && fileBrowserDialog.FileNames.Any())
                    paths = fileBrowserDialog.FileNames.ToList();
            }
            if (paths == null)
                return;
            if (paths.All(fileName => fileName.StartsWith(FolderPath)))
                foreach (var file in paths)
                {
                    var source = file.Substring(FolderPath.Length + 1);
                    var destination = file.Substring(FolderPath.Length + 1);
                    itemSource.Add(FileSystemItem.Create(source, destination));
                }
            else
                _dialogCoordinator.ShowMessageAsync(this, "Error", "Allowed to add files and folders only from the project directory.").Wait();
        }

        protected void TryAddFolders(ObservableCollection<SystemItem> itemSource, List<string> paths)
        {
            if (itemSource == null)
                throw new ArgumentNullException(nameof(itemSource));
            if (paths == null)
                using (var folderBrowserDialog = new FolderBrowserDialog())
                {
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK && Directory.Exists(folderBrowserDialog.SelectedPath))
                        paths = new List<string>
                        {
                            folderBrowserDialog.SelectedPath
                        };
                }
            if (paths == null)
                return;
            if (paths.All(folderName => folderName.StartsWith(FolderPath)))
                foreach (var folder in paths)
                {
                    var directory = folder.Substring(FolderPath.Length + 1);
                    var destination = folder.Substring(FolderPath.Length + 1);
                    itemSource.Add(FolderSystemItem.Create(directory, destination));
                }
            else
                _dialogCoordinator.ShowMessageAsync(this, "Error", "Allowed to add files and folders only from the project directory.").Wait();
        }

        protected bool TryGetImage(out string result)
        {
            var fileBrowserDialog = new OpenFileDialog
            {
                Filter = Resources.ImageFiles,
                CheckFileExists = true
            };
            if (fileBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var selectedPath = fileBrowserDialog.FileName;
                var projectPath = FolderPath + Path.DirectorySeparatorChar;
                string relativePath;
                if (!selectedPath.StartsWith(projectPath))
                {
                    var directoryName = Path.Combine(projectPath, "fomod", "Images");
                    Directory.CreateDirectory(directoryName);
                    var fileNameOnly = Path.GetFileNameWithoutExtension(selectedPath);
                    var extension = Path.GetExtension(selectedPath);
                    var newPath = Path.Combine(directoryName, fileNameOnly + extension);
                    var count = 1;
                    while (File.Exists(newPath))
                        newPath = Path.Combine(directoryName, $"{fileNameOnly}({count++})" + extension);
                    File.Copy(selectedPath, newPath);
                    relativePath = @"fomod\Images\" + Path.GetFileName(newPath);
                }
                else
                    relativePath = selectedPath.Substring(projectPath.Length);
                result = relativePath;
                return true;
            }
            result = string.Empty;
            return false;
        }
    }
}
