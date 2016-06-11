using System.Collections.Generic;
using System.Windows.Input;
using FomodInfrastructure.Interfaces;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
using MahApps.Metro.Controls.Dialogs;

namespace Module.Editor.ViewModel
{
    public class PluginViewModel : FileWorkerBaseViewModel<Plugin>
    {
        public PluginViewModel(IFileBrowserDialog fileBrowserDialog, IFolderBrowserDialog folderBrowserDialog, IDialogCoordinator dialogCoordinator)
            : base(fileBrowserDialog, folderBrowserDialog, dialogCoordinator) {}

        #region Methods

        private void AddImage()
        {
            string imagePath;
            if (TryGetImage(out imagePath))
                Data.Image = Image.Create(imagePath);
        }

        private void RemoveImage()
        {
            Data.Image = null;
        }

        private void BrowseImage()
        {
            string imagePath;
            if (TryGetImage(out imagePath))
                Data.Image.Path = imagePath;
        }

        private void AddFile(List<string> paths)
        {
            AddFile(Data.Files.Items, paths);
        }

        private void AddFolder(List<string> paths)
        {
            AddFolders(Data.Files.Items, paths);
        }

        #endregion

        #region Commands

        private ICommand _addImageCommand;

        public ICommand AddImageCommand
        {
            get { return _addImageCommand ?? (_addImageCommand = new RelayCommand(AddImage)); }
        }

        private ICommand _removeImageCommand;

        public ICommand RemoveImageCommand
        {
            get { return _removeImageCommand ?? (_removeImageCommand = new RelayCommand(RemoveImage)); }
        }

        private ICommand _browseImageCommand;

        public ICommand BrowseImageCommand
        {
            get { return _browseImageCommand ?? (_browseImageCommand = new RelayCommand(BrowseImage)); }
        }

        private ICommand _addFileCommand;

        public ICommand AddFileCommand
        {
            get { return _addFileCommand ?? (_addFileCommand = new RelayCommand<List<string>>(AddFile)); }
        }

        private ICommand _addFolderCommand;

        public ICommand AddFolderCommand
        {
            get { return _addFolderCommand ?? (_addFolderCommand = new RelayCommand<List<string>>(AddFolder)); }
        }

        #endregion
    }
}