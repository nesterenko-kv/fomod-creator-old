using System.Windows.Input;
using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base;
using Prism.Regions;
using FomodInfrastructure;
using Prism.Events;
using Module.Welcome.PrismEvent;

namespace Module.Welcome.ViewModel
{
    public class WelcomeViewModel
    {
        #region Fields

        private ICommand _closeApplication;
        private ICommand _openProject;
        private ICommand _createProject;

        #endregion

        private readonly IAppService _appService;
        private readonly IRegionManager _regionManager;
        private readonly IRepository<ProjectRoot> _repository;
        private readonly IUserMsgService _userMsgService;
        private readonly IEventAggregator _eventAggregator;

        public WelcomeViewModel(IAppService appService, IRepository<ProjectRoot> repository, IRegionManager regionManager, IUserMsgService userMsgService, IEventAggregator eventAggregator)
        {
            _appService = appService;
            _repository = repository;
            _regionManager = regionManager;
            _userMsgService = userMsgService;
            _eventAggregator = eventAggregator;
        }

        #region Commands

        public ICommand CloseApplication
        {
            get
            {
                if (_closeApplication == null)
                    _closeApplication = new RelayCommand(p => _appService.CloseApp());
                return _closeApplication;
            }
        }
        public ICommand OpenProject
        {
            get
            {
                if (_openProject == null)
                    _openProject = new RelayCommand(p =>
                    {
                        var data = _repository.LoadData();

                        if (data!=null)
                        {
                            _appService.InitilizeBaseModules();

                            var param = new NavigationParameters();

                            param.Add(nameof(ProjectRoot.ModuleInformation), data.ModuleInformation);
                            param.Add(nameof(ProjectRoot.ModuleConfiguration), data.ModuleConfiguration);

                            _regionManager.RequestNavigate(Names.MainContentRegion, "InfoEditorView", param);

                            _eventAggregator.GetEvent<OpenProjectEvent>().Publish(data.FolderPath);

                            foreach (var item in _regionManager.Regions[Names.TopRegion].Views)
                                _regionManager.Regions[Names.TopRegion].Remove(item);
                        }
                        else
                        {
                            _userMsgService.Send("Указанная папка не соответствует необходимым требованиям.");
                        }
                       
                    });
                return _openProject;
            }
        }


        public ICommand CreateProject
        {
            get
            {
                if (_createProject == null)
                    _createProject = new RelayCommand(p =>
                    {
                        
                    });
                return _createProject;
            }
        }
        #endregion
    }
}
