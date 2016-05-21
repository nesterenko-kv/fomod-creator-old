using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base;
using FomodModel.Base.ModuleCofiguration;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using FomodInfrastructure.Interface;
using System;

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
        public RelayCommand<CompositeDependency> AddCompositeDependencyCommand { get; }
        public RelayCommand<CompositeDependency> RemoveCompositeDependencyCommand { get; }
        public RelayCommand<CompositeDependency> AddFileDependencyCommand { get; }
        public RelayCommand<CompositeDependency> AddFlagDependencyCommand { get; }
        public RelayCommand<FileDependency> RemoveFileDependencyCommand { get; }
        public RelayCommand<FlagDependency> RemoveFlagDependencyCommand { get; }
        public RelayCommand<object> AddFileCommand { get; }
        public RelayCommand<object> AddFolderCommand { get; }
        public RelayCommand<SystemItem> RemoveSystemItemCommand { get; }

        public RelayCommand AddConditionalFileInstallsCommand { get; }

        #endregion

        private ProjectRoot _data;

        public ProjectRootViewModel(IFileBrowserDialog fileBrowserDialog)
        {
            _fileBrowserDialog = fileBrowserDialog;
            AddImageCommand = new RelayCommand(AddImage);
            RemoveImageCommand = new RelayCommand(RemoveImage);
            BrowseImageCommand = new RelayCommand(BrowseImage);
            AddCompositeDependencyCommand = new RelayCommand<CompositeDependency>(AddCompositeDependency);
            RemoveCompositeDependencyCommand = new RelayCommand<CompositeDependency>(RemoveCompositeDependency);
            AddFileDependencyCommand = new RelayCommand<CompositeDependency>(AddFileDependency);
            RemoveFileDependencyCommand = new RelayCommand<FileDependency>(RemoveFileDependency);
            AddFlagDependencyCommand = new RelayCommand<CompositeDependency>(AddFlagDependency);
            RemoveFlagDependencyCommand = new RelayCommand<FlagDependency>(RemoveFlagDependency);
            AddFileCommand = new RelayCommand<object>(AddFile);
            AddFolderCommand = new RelayCommand<object>(AddFolder);
            RemoveSystemItemCommand = new RelayCommand<SystemItem>(RemoveSystemItem);
            AddConditionalFileInstallsCommand = new RelayCommand(AddConditionalFileInstalls);

            // ReSharper disable once SuspiciousTypeConversion.Global - аспект решает.
            var notifyPropertyChanged = this as INotifyPropertyChanged;
            if (notifyPropertyChanged != null)
                notifyPropertyChanged.PropertyChanged +=
                    (obj, args) => _data = args.PropertyName == nameof(Data) ? (ProjectRoot) Data : _data;
        }

        private void AddConditionalFileInstalls()
        {
            if (_data.ModuleConfiguration.ConditionalFileInstalls == null) _data.ModuleConfiguration.ConditionalFileInstalls = new ConditionalFileInstallList
            {
                Patterns = new ObservableCollection<ConditionalInstallPattern>()
            };
            _data.ModuleConfiguration.ConditionalFileInstalls.Patterns.Add(new ConditionalInstallPattern
            {
                //Dependencies = new CompositeDependency()
            });
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
            var imagepath = GetImage();
            if (!string.IsNullOrEmpty(imagepath))
                _data.ModuleConfiguration.ModuleImage.Path = imagepath;
        }

        private void AddImage()
        {
            var imagepath = GetImage();
            if (!string.IsNullOrEmpty(imagepath))
                _data.ModuleConfiguration.ModuleImage = new HeaderImage { Path = imagepath };
        }

        private void RemoveImage()
        {
            _data.ModuleConfiguration.ModuleImage = null;
        }

        private void AddCompositeDependency(CompositeDependency dependency)
        {
            if (dependency == null)
                _data.ModuleConfiguration.ModuleDependencies = CompositeDependency.Create();
            else
                dependency.Dependencies = CompositeDependency.Create();
        }

        private void RemoveCompositeDependency(CompositeDependency dependency)
        {
            if (dependency.Parent == null)
                _data.ModuleConfiguration.ModuleDependencies = null;
            else
                dependency.Parent.Dependencies = null;
        }

        private void AddFileDependency(CompositeDependency dependency)
        {
            var list = dependency.FileDependencies;
            if (list == null)
                list = new ObservableCollection<FileDependency>();
            list.Add(FileDependency.Create("aa/ds2/fdf.cc"));
        }

        private void AddFlagDependency(CompositeDependency dependency)
        {
            var list = dependency.FlagDependencies;
            if (list == null)
                list = new ObservableCollection<FlagDependency>();
            list.Add(FlagDependency.Create());
        }

        private void RemoveFileDependency(FileDependency dependency)
        {
            dependency.Parent.FileDependencies.Remove(dependency);
            dependency.Parent = null;
        }

        private void RemoveFlagDependency(FlagDependency dependency)
        {
            dependency.Parent.FlagDependencies.Remove(dependency);
            dependency.Parent = null;
        }


        private void AddFile(object obj)
        {
            if (_data.ModuleConfiguration.RequiredInstallFiles == null)
                _data.ModuleConfiguration.RequiredInstallFiles = new FileList();

            _data.ModuleConfiguration.RequiredInstallFiles.Items.Add(new FileSystemItem
            {
                Source = @"\ffga\kfdd.exe",
                Destination = @"\kfdd.exe",
                AlwaysInstall = false,
                InstallIfUsable = false,
                Priority = "0"
            });
        }
        private void AddFolder(object obj)
        {
            if (_data.ModuleConfiguration.RequiredInstallFiles == null)
                _data.ModuleConfiguration.RequiredInstallFiles = new FileList();

            _data.ModuleConfiguration.RequiredInstallFiles.Items.Add(new FolderSystemItem
            {
                Source = @"\ffga\folder\1\folderNew",
                Destination = @"\folderNew",
                AlwaysInstall = false,
                InstallIfUsable = false,
                Priority = "0"
            });
        }

        private void RemoveSystemItem(SystemItem systemItem)
        {
            _data.ModuleConfiguration.RequiredInstallFiles.Items.Remove(systemItem);
        }
        
        #endregion
    }
}