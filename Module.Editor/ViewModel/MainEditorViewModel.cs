using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AspectInjector.Broker;
using FomodInfrastructure;
using FomodInfrastructure.Aspect;
using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base;
using FomodModel.Base.ModuleCofiguration;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;

namespace Module.Editor.ViewModel
{
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    public class MainEditorViewModel : IDisposable
    {
        public MainEditorViewModel(IMemoryService memoryService, ILogger logger)
        {
            _memoryService = memoryService;
            _logger = logger;
            _logger.LogCreate(this);
        }

        public void Dispose()
        {
            var list = _regionManager.Regions[Names.NodeRegion].Views.ToList();
            foreach (var item in list)
            {
                ((FrameworkElement)item).DataContext = null;
                _regionManager.Regions[Names.NodeRegion].Remove(item);
            }
        }

        ~MainEditorViewModel()
        {
            _logger.LogDisposable(this);
        }

        public void ConfigurateViewModel(IRegionManager regionManager, IRepository<ProjectRoot> repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));
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

        #region Services

        private IRegionManager _regionManager;

        private IRepository<ProjectRoot> _repository;

        private readonly IMemoryService _memoryService;

        private readonly ILogger _logger;

        #endregion

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

        public InteractionRequest<IConfirmation> ConfirmationRequest { get; } = new InteractionRequest<IConfirmation>();

        public ObservableCollection<ProjectRoot> Data { get; } = new ObservableCollection<ProjectRoot>();

        public ProjectRoot FirstData { get; private set; }

        //TODO: Не забыть про очищение свойства при закрытии или удалении проекта, или сделать слабую ссылку

        private object _selectedNode;

        public object SelectedNode
        {
            get { return _selectedNode; }
            set
            {
                _selectedNode = value;
                if (value == null)
                    return;
                var name = value.GetType().Name;
                var param = new NavigationParameters { { name, value }, { "FolderPath", _repository.CurrentPath } };
                _regionManager.Regions[Names.NodeRegion].RequestNavigate(name + "View", param);
            }
        }

        #endregion

        #region Commands

        private ICommand _addStepCommand;

        public ICommand AddStepCommand
        {
            get
            {
                return _addStepCommand ?? (_addStepCommand = new RelayCommand<ProjectRoot>(p =>
                {
                    if (p.ModuleConfiguration.InstallSteps == null)
                        p.ModuleConfiguration.InstallSteps = StepList.Create();
                    if (p.ModuleConfiguration.InstallSteps.InstallStep == null)
                        p.ModuleConfiguration.InstallSteps.InstallStep = new ObservableCollection<InstallStep>();
                    p.ModuleConfiguration.InstallSteps.InstallStep.Add(InstallStep.Create());
                }));
            }
        }

        private ICommand _addGroupCommand;

        public ICommand AddGroupCommand
        {
            get
            {
                return _addGroupCommand ?? (_addGroupCommand = new RelayCommand<InstallStep>(p =>
                {
                    if (p.OptionalFileGroups == null)
                        p.OptionalFileGroups = GroupList.Create();
                    if (p.OptionalFileGroups.Group == null)
                        p.OptionalFileGroups.Group = new ObservableCollection<Group>();
                    p.OptionalFileGroups.Group.Add(Group.Create());
                }));
            }
        }

        private ICommand _addPluginCommand;

        public ICommand AddPluginCommand
        {
            get
            {
                return _addPluginCommand ?? (_addPluginCommand = new RelayCommand<Group>(p =>
                {
                    if (p.Plugins == null)
                        p.Plugins = PluginList.Create();
                    if (p.Plugins.Plugin == null)
                        p.Plugins.Plugin = new ObservableCollection<Plugin>();
                    p.Plugins.Plugin.Add(Plugin.Create());
                }));
            }
        }

        private ICommand _deleteDialogCommand;

        public ICommand DeleteDialogCommand
        {
            get { return _deleteDialogCommand ?? (_deleteDialogCommand = new RelayCommand<object[]>(DeleteDialog)); }
        }

        #endregion

        #region Methods

        public void Save()
        {
            _repository.SaveData(_repository.CurrentPath);
        }

        public void SaveAs()
        {
            _repository.SaveData();
        }

        private void DeleteDialog(object[] objects)
        {
            //TODO: Localize
            ConfirmationRequest.Raise(new Confirmation { Content = "You sure you want remove a node ?", Title = "Удалить узел?" }, c =>
            {
                if (!c.Confirmed)
                    return;
                if (objects[1] is InstallStep)
                {
                    var root = (ProjectRoot)objects[0];
                    var step = (InstallStep)objects[1];
                    root.ModuleConfiguration.InstallSteps.InstallStep.Remove(step);
                }
                else
                {
                    if (objects[1] is Group)
                    {
                        var step = (InstallStep)objects[0];
                        var group = (Group)objects[1];
                        step.OptionalFileGroups.Group.Remove(@group);
                    }
                    else
                    {
                        if (objects[1] is Plugin)
                        {
                            var group = (Group)objects[0];
                            var plugin = (Plugin)objects[1];
                            group.Plugins.Plugin.Remove(plugin);
                        }
                    }
                }
            });
        }

        #endregion
    }
}