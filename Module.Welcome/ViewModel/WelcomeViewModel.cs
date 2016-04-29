using System.Windows.Input;
using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base;
using Prism.Regions;
using FomodInfrastructure;
using Prism.Events;
using Module.Welcome.PrismEvent;
using System.Xml.Linq;
using MahApps.Metro.Controls.Dialogs;

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
        private readonly IRepository<XElement> _repositoryXml;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IEventAggregator _eventAggregator;

        public WelcomeViewModel(IAppService appService, IRepository<ProjectRoot> repository, IRepository<XElement> repositoryXml, IRegionManager regionManager, IDialogCoordinator dialogCoordinator, IEventAggregator eventAggregator)
        {
            _appService = appService;
            _repository = repository;
            _regionManager = regionManager;
            _dialogCoordinator = dialogCoordinator;
            _eventAggregator = eventAggregator;
            _repositoryXml = repositoryXml;

            _eventAggregator.GetEvent<OpenLink>().Subscribe(p =>
            {
                OpenProject.Execute(p);
            });
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
                        ProjectRoot data;
                        XElement X = null;
                        if (p == null)
                        {
                            data = _repository.LoadData();
                        }
                        else
                        {
                            data = _repository.LoadData(p.ToString());
                            X = _repositoryXml.LoadData(p.ToString());
                        }

                        if (data != null)
                        {
                            _appService.InitilizeBaseModules();
                            var param = new NavigationParameters
                            {
                                {nameof(ProjectRoot.ModuleInformation), data.ModuleInformation},
                                {nameof(ProjectRoot.ModuleConfiguration), data.ModuleConfiguration},
                                {"xml", X},
                                {"folderPath", data.FolderPath}
                            };
                            _regionManager.RequestNavigate(Names.MainContentRegion, "InfoEditorView", param);
                            _regionManager.RequestNavigate(Names.MainContentRegion, "MainEditorView", param);

                            _eventAggregator.GetEvent<OpenProjectEvent>().Publish(data.FolderPath);

                            foreach (var item in _regionManager.Regions[Names.TopRegion].Views)
                                _regionManager.Regions[Names.TopRegion].Deactivate(item);
                        }
                        else
                            _dialogCoordinator.ShowMessageAsync(this, "Ошибка",
                                "Указанная папка не соответствует необходимым требованиям.");
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
