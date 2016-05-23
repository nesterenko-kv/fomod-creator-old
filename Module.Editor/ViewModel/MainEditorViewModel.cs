using System.Collections.ObjectModel;
using System.Linq;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base;
using FomodModel.Base.ModuleCofiguration;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using FomodInfrastructure.Interface;

namespace Module.Editor.ViewModel
{
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    public class MainEditorViewModel : BindableBase
    {
        #region Services

        private IRegionManager _regionManager;
        private IRepository<ProjectRoot> _repository;
        private readonly IMemoryService _memoryService;

        #endregion

        public MainEditorViewModel(IMemoryService memoryService)
        {
            _memoryService = memoryService;

            AddStepCommand = new RelayCommand<ProjectRoot>(AddStep);
            AddGroupCommand = new RelayCommand<InstallStep>(AddGroup);
            AddPluginCommand = new RelayCommand<Group>(AddPlugin);
            DeleteDialogCommand = new RelayCommand<object[]>(DeleteDialog);
        }
        
        public void ConfigurateViewModel(IRegionManager regionManager, IRepository<ProjectRoot> repository)
        {
            if (repository == null) throw new NotImplementedException();
            _repository = repository;
            _regionManager = regionManager;
            var root = Data.FirstOrDefault(i => i.FolderPath == repository.CurrentPath);
            if (root == null)
            {
                Data.Add(repository.GetData());
                if (FirstData == null)
                    FirstData = repository.GetData();
            }
            _memoryService.GetMemorySize(Data);
            SelectedNode = FirstData;
        }

        #region Properties

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

        public InteractionRequest<IConfirmation> ConfirmationRequest { get; private set; } = new InteractionRequest<IConfirmation>();

        public ObservableCollection<ProjectRoot> Data { get; set; } = new ObservableCollection<ProjectRoot>();

        public ProjectRoot FirstData { get; private set; } //TODO Не забыть про очищение свойства при закрытии или удалении проекта, или сделать слабую ссылку

        private object _selectedNode;

        public object SelectedNode
        {
            get { return _selectedNode; }
            set
            {
                _selectedNode = value;
                if (value == null) return;
                var name = value.GetType().Name;
                var param = new NavigationParameters
                {
                    {name, value}
                };
                _regionManager.Regions["NodeRegion"].RequestNavigate(name + "View", param);
            }
        }
        
        #endregion

        #region Commands

        public RelayCommand<ProjectRoot> AddStepCommand { get; }
        public RelayCommand<InstallStep> AddGroupCommand { get; }
        public RelayCommand<Group> AddPluginCommand { get; }
        public RelayCommand<object[]> DeleteDialogCommand { get; private set; }

        #endregion

        #region Methods

        private void DeleteDialog(object[] obj)
        {
            var item = obj[1];
            ConfirmationRequest.Raise(new Confirmation { Content = "Вы точно хотите удалить " + item.GetType().Name + " узел?", Title = "Удалить узел?" }, c =>
            {
                if (!c.Confirmed) return;
                if (item is Plugin)
                    RemovePlugin(obj);
                else if (item is Group)
                    RemoveGroup(obj);
                else if (item is InstallStep)
                    RemoveStep(obj);
                else
                    throw new NotImplementedException();
            });
        }

        private void AddStep(ProjectRoot p)
        {
            var steps = p.ModuleConfiguration.InstallSteps;
            if (steps == null)
                steps = new StepList();
            if (steps.InstallStep == null)
                steps.InstallStep = new ObservableCollection<InstallStep>();
            steps.InstallStep.Add(InstallStep.Create());
        }

        private void AddGroup(InstallStep p)
        {
            var groups = p.OptionalFileGroups;
            if (groups == null)
                groups = new GroupList();
            if (groups.Group == null)
                groups.Group = new ObservableCollection<Group>();
            groups.Group.Add(Group.Create());
        }

        private void AddPlugin(Group p)
        {
            var plugins = p.Plugins;
            if (plugins == null)
                plugins = new PluginList();
            if (plugins.Plugin == null)
                plugins.Plugin = new ObservableCollection<Plugin>();
            plugins.Plugin.Add(Plugin.Create());
        }

        private void RemoveStep(IReadOnlyList<object> p)
        {
            var root = (ProjectRoot)p[0];
            var step = (InstallStep)p[1];
            root.ModuleConfiguration.InstallSteps.InstallStep.Remove(step);
        }

        private void RemoveGroup(object[] p)
        {
            var step = (InstallStep)p[0];
            var group = (Group)p[1];
            step.OptionalFileGroups.Group.Remove(group);
        }

        private void RemovePlugin(object[] p)
        {
            var group = (Group)p[0];
            var plugin = (Plugin)p[1];
            group.Plugins.Plugin.Remove(plugin);
        }

        public void Save()
        {
            _repository.SaveData(_repository.CurrentPath);
        }

        public void SaveAs()
        {
            _repository.SaveData();
        }

        #endregion
    }
}