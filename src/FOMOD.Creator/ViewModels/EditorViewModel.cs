namespace FOMOD.Creator.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Input;
    using FOMOD.Creator.Commands;
    using FOMOD.Creator.Domain;
    using FOMOD.Creator.Domain.Models;
    using FOMOD.Creator.Domain.Models.ModuleCofiguration;
    using FOMOD.Creator.Interfaces;
    using Prism.Interactivity.InteractionRequest;
    using Prism.Regions;
    using PropertyChanged;

    [ImplementPropertyChanged]
    public class EditorViewModel : ICloseable, IHaveDisplayName
    {
        private readonly IMemoryService _memoryService;
        private ICommand _addGroupCommand;
        private ICommand _addPluginCommand;
        private ICommand _addStepCommand;
        private ICommand _deleteDialogCommand;
        private IRegionManager _regionManager;
        private IProjectManager _repository;

        public EditorViewModel(IMemoryService memoryService)
        {
            _memoryService = memoryService;
        }

        public ICommand AddGroupCommand
        {
            get
            {
                return _addGroupCommand ?? (_addGroupCommand = new RelayCommand<InstallStep>(AddGroup));
            }
        }

        public ICommand AddPluginCommand
        {
            get
            {
                return _addPluginCommand ?? (_addPluginCommand = new RelayCommand<Group>(AddPlugin));
            }
        }

        public ICommand AddStepCommand
        {
            get
            {
                return _addStepCommand ?? (_addStepCommand = new RelayCommand<Project>(AddStep));
            }
        }

        public InteractionRequest<IConfirmation> ConfirmationRequest { get; } = new InteractionRequest<IConfirmation>();

        public Project Data { get; private set; }

        public ICommand DeleteDialogCommand
        {
            get
            {
                return _deleteDialogCommand ?? (_deleteDialogCommand = new RelayCommand<object[]>(DeleteDialog));
            }
        }

        public string DisplayName => Data?.Info?.Name;

        public bool IsNeedSave
        {
            get
            {
                _memoryService.GetMemorySize(Data);
                return _memoryService.IsMemorySizeChanged;
            }
            set
            {
                if (!value)
                    _memoryService.Reset(Data);
            }
        }

        [DependsOn(nameof(Data))]
        public IEnumerable<Project> Items
        {
            get
            {
                yield return Data;
            }
        }

        public object SelectedNode { get; set; }

        public void Close()
        {
            foreach (var item in _regionManager.Regions[Names.NodeRegion].Views)
            {
                ((FrameworkElement) item).DataContext = null;
                _regionManager.Regions[Names.NodeRegion].Remove(item);
            }
        }

        public void ConfigureViewModel(IRegionManager regionManager, IProjectManager repository)
        {
            if (regionManager == null)
                throw new ArgumentNullException(nameof(regionManager));
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));
            _repository = repository;
            _regionManager = regionManager;
            if (Data == null || Data.Source == repository.Data.Source)
                Data = repository.Data;
            _memoryService.GetMemorySize(Data);
            SelectedNode = Data;
        }

        public void OnSelectedNodeChanged()
        {
            if (SelectedNode == null)
                return;
            var name = SelectedNode.GetType().Name;
            var param = new NavigationParameters
            {
                {
                    name, SelectedNode
                },
                {
                    "FolderPath", _repository.Data?.Source
                }
            };
            _regionManager.Regions[Names.NodeRegion].RequestNavigate(name + "View", param);
        }

        public void Save()
        {
            _repository.Save(_repository.Data.Source);
        }

        public void SaveAs()
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            var result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK && Directory.Exists(folderBrowserDialog.SelectedPath))
                _repository.Save(folderBrowserDialog.SelectedPath);
        }

        private static void AddGroup(InstallStep p)
        {
            if (p.OptionalFileGroups == null)
                p.OptionalFileGroups = new GroupList();
            if (p.OptionalFileGroups.Group == null)
                p.OptionalFileGroups.Group = new ObservableCollection<Group>();
            p.OptionalFileGroups.Group.Add(Group.Create());
        }

        private static void AddPlugin(Group p)
        {
            if (p.Plugins == null)
                p.Plugins = new PluginList();
            if (p.Plugins.Plugin == null)
                p.Plugins.Plugin = new ObservableCollection<Plugin>();
            p.Plugins.Plugin.Add(Plugin.Create());
        }

        private static void AddStep(Project p)
        {
            if (p.Config.InstallSteps == null)
                p.Config.InstallSteps = new StepList();
            if (p.Config.InstallSteps.InstallStep == null)
                p.Config.InstallSteps.InstallStep = new ObservableCollection<InstallStep>();
            p.Config.InstallSteps.InstallStep.Add(InstallStep.Create());
        }

        private void DeleteDialog(object[] objects)
        {
            ConfirmationRequest.Raise(new Confirmation
            {
                Content = "Are you sure that you wanna delete node?",
                Title = "Delete node"
            }, c =>
            {
                if (!c.Confirmed)
                    return;
                var installStep = objects[1] as InstallStep;
                if (installStep != null)
                {
                    var root = (Project) objects[0];
                    root.Config.InstallSteps.InstallStep.Remove(installStep);
                }
                else
                {
                    var group = objects[1] as Group;
                    if (group != null)
                    {
                        var step = (InstallStep) objects[0];
                        step.OptionalFileGroups.Group.Remove(group);
                    }
                    else
                    {
                        var plugin = objects[1] as Plugin;
                        if (plugin != null)
                        {
                            group = (Group) objects[0];
                            group.Plugins.Plugin.Remove(plugin);
                        }
                    }
                }
            });
        }
    }
}
