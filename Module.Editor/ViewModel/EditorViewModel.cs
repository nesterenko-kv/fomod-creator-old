using System.Collections.ObjectModel;
using System.Linq;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodInfrastructure.Interface;
using FomodModel.Base;
using Prism.Mvvm;
using Prism.Regions;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
// ReSharper disable MemberCanBePrivate.Global

namespace Module.Editor.ViewModel
{
    [Aspect(typeof (AspectINotifyPropertyChanged))]
    public class EditorViewModel : BindableBase
    {
        public EditorViewModel(IRepository<ProjectRoot> repository)
        {
            _repository = repository;

            AddStep = new RelayCommand<ProjectRoot>(p =>
            {
                if (p.ModuleConfiguration.InstallSteps == null)
                    p.ModuleConfiguration.InstallSteps = new StepList();
                if (p.ModuleConfiguration.InstallSteps.InstallStep == null)
                    p.ModuleConfiguration.InstallSteps.InstallStep = new ObservableCollection<InstallStep>();
                p.ModuleConfiguration.InstallSteps.InstallStep.Add(new InstallStep {Name = "NewStep"});
            });

        }

        public string Header { get; private set; } = "Редактор";

        public void ConfigurateViewModel(IRegionManager regionManager, ProjectRoot projectRoot, string header = null)
        {
            if (header != null)
                Header = header;
            else
            {
                var name = projectRoot.ModuleInformation.Name;
                Header = string.IsNullOrWhiteSpace(name) ? Header : name;
            }

            var pRoot = Data.FirstOrDefault(i => i.FolderPath == projectRoot.FolderPath);
            if (pRoot == null)
                Data.Add(projectRoot);


            _regionManager = regionManager;
        }

        #region Properties

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

        public ObservableCollection<ProjectRoot> Data { get; set; } = new ObservableCollection<ProjectRoot>();

        #endregion

        #region Services

        private readonly IRepository<ProjectRoot> _repository;
        private IRegionManager _regionManager;

        #endregion


        #region Commands

        public RelayCommand<ProjectRoot> AddStep { get; }
        public RelayCommand<ProjectRoot> AddGroup { get; }

        #endregion
    }
}