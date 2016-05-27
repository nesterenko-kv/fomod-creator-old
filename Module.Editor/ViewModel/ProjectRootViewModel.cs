using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base;
using FomodModel.Base.ModuleCofiguration;
using System.IO;
using System.Windows.Input;
using FomodInfrastructure.Interface;

namespace Module.Editor.ViewModel
{
    public class ProjectRootViewModel : BaseViewModel<ProjectRoot>
    {
        #region Services

        private readonly IFileBrowserDialog _fileBrowserDialog;

        #endregion

        #region Commands

        private ICommand _addImageCommand;
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
        private ICommand _removeImageCommand;
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
        private ICommand _browseImageCommand;
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

        #endregion

        public ProjectRootViewModel(IFileBrowserDialog fileBrowserDialog)
        {
            _fileBrowserDialog = fileBrowserDialog;
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