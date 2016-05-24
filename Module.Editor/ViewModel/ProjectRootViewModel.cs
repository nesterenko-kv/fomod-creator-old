using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base;
using FomodModel.Base.ModuleCofiguration;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using FomodInfrastructure.Interface;
using System.Linq;
using System.Collections;

namespace Module.Editor.ViewModel
{
    public class ProjectRootViewModel : BaseViewModel
    {
        #region Services

        private readonly IFileBrowserDialog _fileBrowserDialog;

        #endregion

     
        #region Commands

        public RelayCommand AddImageCommand { get; }
        public RelayCommand RemoveImageCommand { get; }
        public RelayCommand BrowseImageCommand { get; }

        #endregion

        private ProjectRoot _data;

        public ProjectRootViewModel(IFileBrowserDialog fileBrowserDialog)
        {
            _fileBrowserDialog = fileBrowserDialog;
            AddImageCommand = new RelayCommand(AddImage);
            RemoveImageCommand = new RelayCommand(RemoveImage);
            BrowseImageCommand = new RelayCommand(BrowseImage);
            var notifyPropertyChanged = this as INotifyPropertyChanged;
            if (notifyPropertyChanged != null)
                notifyPropertyChanged.PropertyChanged +=
                    (obj, args) => _data = args.PropertyName == nameof(Data) ? (ProjectRoot) Data : _data;
        }


        #region Methods


        private string GetImage()
        {
            _fileBrowserDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            _fileBrowserDialog.ShowDialog();
            var selectedPath = _fileBrowserDialog.SelectedPath;
            if (!File.Exists(selectedPath)) return string.Empty;
            _fileBrowserDialog.Reset();

            var projectPath = _data.FolderPath + Path.DirectorySeparatorChar;
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

        private void BrowseImage()
        {
            var imagePath = GetImage();
            if (!string.IsNullOrEmpty(imagePath))
                _data.ModuleConfiguration.ModuleImage.Path = imagePath;
        }

        private void AddImage()
        {
            var imagePath = GetImage();
            if (!string.IsNullOrEmpty(imagePath))
                _data.ModuleConfiguration.ModuleImage = HeaderImage.Create(imagePath);
        }

        private void RemoveImage()
        {
            _data.ModuleConfiguration.ModuleImage = null;
        }

        
        #endregion
    }
}