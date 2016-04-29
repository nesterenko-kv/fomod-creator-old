using System.Windows.Input;
using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary.Commands;
using Prism.Regions;
using FomodInfrastructure;
using Prism.Events;
using Module.Welcome.PrismEvent;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Data;

namespace Module.Welcome.ViewModel
{
    public class WelcomeViewModel
    {
        #region Services

        private readonly IAppService _appService;
        private readonly IRegionManager _regionManager;
        private readonly IRepository<XmlDataProvider> _repositoryXml;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IEventAggregator _eventAggregator;

        #endregion

        #region Commands

        private ICommand _closeApplication;
        private ICommand _openProject;
        private ICommand _createProject;

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
                        var x = p == null ? _repositoryXml.LoadData() : _repositoryXml.LoadData(p.ToString());
                        if (x != null)
                        {
                            _appService.InitilizeBaseModules();
                            var param = new NavigationParameters
                            {
                                {"xml", x},
                                {"folderPath", p?.ToString()}
                            };
                            _regionManager.RequestNavigate(Names.MainContentRegion, "InfoEditorView", param);
                            _regionManager.RequestNavigate(Names.MainContentRegion, "MainEditorView", param);

                            _eventAggregator.GetEvent<OpenProjectEvent>().Publish(_repositoryXml.CurrentPath);

                            foreach (var item in _regionManager.Regions[Names.TopRegion].Views)
                                _regionManager.Regions[Names.TopRegion].Deactivate(item);
                        }
                        else
                            _dialogCoordinator.ShowMessageAsync(this, "Ошибка", "Указанная папка не соответствует необходимым требованиям.");
                    });
                return _openProject;
            }
        }

        public ICommand CreateProject
        {
            get
            {
                if (_createProject == null)
                    _createProject = new RelayCommand(p => {});
                return _createProject;
            }
        }

        #endregion

        public WelcomeViewModel(IAppService appService, IRepository<XmlDataProvider> repositoryXml, IRegionManager regionManager, IDialogCoordinator dialogCoordinator, IEventAggregator eventAggregator)
        {
            _appService = appService;
            _regionManager = regionManager;
            _dialogCoordinator = dialogCoordinator;
            _eventAggregator = eventAggregator;
            _repositoryXml = repositoryXml;
            _eventAggregator.GetEvent<OpenLink>().Subscribe(p => OpenProject.Execute(p));
        }

    }
}
