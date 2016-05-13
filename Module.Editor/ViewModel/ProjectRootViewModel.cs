using System;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base;
using FomodModel.Base.ModuleCofiguration;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Module.Editor.ViewModel
{
    public class ProjectRootViewModel : BaseViewModel
    {
        private ProjectRoot _data;

        public RelayCommand AddImageCommand { get; }
        public RelayCommand RemoveImageCommand { get; }
        public RelayCommand<CompositeDependency> AddCompositeDependencyCommand { get; }
        public RelayCommand<CompositeDependency> RemoveCompositeDependencyCommand { get; }

        public RelayCommand<CompositeDependency> AddFileDependencyCommand { get; }
        public RelayCommand<CompositeDependency> AddFlagDependencyCommand { get; }
        public RelayCommand<FileDependency> RemoveFileDependencyCommand { get; }
        public RelayCommand<FlagDependency> RemoveFlagDependencyCommand { get; }
        

        public ProjectRootViewModel()
        {
            AddImageCommand = new RelayCommand(AddImage);
            RemoveImageCommand = new RelayCommand(RemoveImage);

            AddCompositeDependencyCommand = new RelayCommand<CompositeDependency>(AddCompositeDependency);
            RemoveCompositeDependencyCommand = new RelayCommand<CompositeDependency>(RemoveCompositeDependency);

            AddFileDependencyCommand = new RelayCommand<CompositeDependency>(AddFileDependency);
            RemoveFileDependencyCommand = new RelayCommand<FileDependency>(RemoveFileDependency);

            AddFlagDependencyCommand = new RelayCommand<CompositeDependency>(AddFlagDependency);
            RemoveFlagDependencyCommand = new RelayCommand<FlagDependency>(RemoveFlagDependency);

            (this as INotifyPropertyChanged).PropertyChanged += (obj, args) =>
            _data = args.PropertyName == nameof(Data) ? (ProjectRoot)Data : _data;
  
        }



        private void AddImage()
        {
            _data.ModuleConfiguration.ModuleImage = new HeaderImage
            {
                Path = "aaaa/dsdsd/dasd",
                ShowFade = false,
                ShowImage = true
            };
        }

        private void RemoveImage()
        {
            _data.ModuleConfiguration.ModuleImage = null;
        }

        private void AddCompositeDependency(CompositeDependency dependency)
        {
            if (dependency==null)
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
            if (list == null) list = new ObservableCollection<FileDependency>();

            list.Add(FileDependency.Create("aa/ds2/fdf.cc"));
        }

        private void AddFlagDependency(CompositeDependency dependency)
        {

            var list = dependency.FlagDependencies;
            if (list == null) list = new ObservableCollection<FlagDependency>();

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
    }
}