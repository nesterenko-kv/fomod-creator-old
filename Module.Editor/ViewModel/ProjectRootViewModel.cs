using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base;
using FomodModel.Base.ModuleCofiguration;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using FomodInfrastructure.Interface;
using System;
using System.Linq;
using System.Collections;

namespace Module.Editor.ViewModel
{
    public class ProjectRootViewModel : BaseViewModel
    {
        #region Services

        private readonly IFileBrowserDialog _fileBrowserDialog;

        #endregion


        RelayCommand _createFileLisCommand; public RelayCommand CreateFileLisCommand
        {
            get
            {
                return _createFileLisCommand ?? (_createFileLisCommand = new RelayCommand(() =>
                {
                    _data.ModuleConfiguration.CreatRequiredInstallFiles();
                }));
            }
        }


        RelayCommand _removeFileLisCommand; public RelayCommand RemoveFileLisCommand
        {
            get
            {
                return _removeFileLisCommand ?? (_removeFileLisCommand = new RelayCommand(() =>
                {
                    _data.ModuleConfiguration.RemoveRequiredInstallFiles();
                }));
            }
        }


        RelayCommand<ConditionalInstallPattern> _createPaternCommand; public RelayCommand<ConditionalInstallPattern> CreatePaternCommand
        {
            get
            {
                return _createPaternCommand ?? (_createPaternCommand = new RelayCommand<ConditionalInstallPattern>((patern) =>
                {
                    patern.CreateFilesList();
                }));
            }
        }

        RelayCommand<ConditionalInstallPattern> _removePaternCommand; public RelayCommand<ConditionalInstallPattern> RemovePaternCommand
        {
            get
            {
                return _removePaternCommand ?? (_removePaternCommand = new RelayCommand<ConditionalInstallPattern>((patern) =>
                {
                    patern.RemoveFilesList();
                }));
            }
        }

        #region Commands

        public RelayCommand AddImageCommand { get; }
        public RelayCommand RemoveImageCommand { get; }
        public RelayCommand BrowseImageCommand { get; }
        public RelayCommand<CompositeDependency> AddCompositeDependencyCommand { get; }
        public RelayCommand<CompositeDependency> RemoveCompositeDependencyCommand { get; }
        public RelayCommand<object> AddCompositeDependencyCommand2 { get; }
        public RelayCommand<CompositeDependency> RemoveCompositeDependencyCommand2 { get; }
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

            //зависимости для ConditionalFileInstallsControl
            AddCompositeDependencyCommand2 = new RelayCommand<object>(AddCompositeDependency2);
            RemoveCompositeDependencyCommand2 = new RelayCommand<CompositeDependency>(RemoveCompositeDependency2);


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
            _data.ModuleConfiguration.CreatConditionalFileInstalls();
            _data.ModuleConfiguration.ConditionalFileInstalls.AddPatern(new ConditionalInstallPattern
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


        private void AddCompositeDependency2(object dependency)
        {
            if (dependency is ConditionalInstallPattern)
                (dependency as ConditionalInstallPattern).Dependencies = CompositeDependency.Create();//TODO Немного говнокода который завязан на ткущем представлении вьюх
            else if (dependency is CompositeDependency)
                (dependency as CompositeDependency).Dependencies = CompositeDependency.Create();
        }
        private void RemoveCompositeDependency2(CompositeDependency dependency)
        {
            if (dependency.Parent == null)
                _data.ModuleConfiguration.ConditionalFileInstalls.Patterns.First(i=>i.Dependencies == dependency).Dependencies = null;
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
            if(obj is IList)
            {
                (obj as IList).Add(new FileSystemItem
                {
                    Source = @"\ffga\kfdd.exe",
                    Destination = @"\kfdd.exe",
                    AlwaysInstall = false,
                    InstallIfUsable = false,
                    Priority = "0"
                });
                return;
            }
        }
        private void AddFolder(object obj)
        {
            if (obj is IList)
            {
                (obj as IList).Add(new FolderSystemItem
                {
                    Source = @"\ffga\folder\1\folderNew",
                    Destination = @"\folderNew",
                    AlwaysInstall = false,
                    InstallIfUsable = false,
                    Priority = "0"
                });
                return;
            }
        }

        private void RemoveSystemItem(SystemItem systemItem)
        {
            systemItem.Parent.Remove(systemItem);
        }
        
        #endregion
    }
}