using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary.Commands;
//using Prism.Regions;
using Prism.Events;
using Module.Welcome.PrismEvent;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Data;

namespace Module.Welcome.ViewModel
{
    public class WelcomeViewModel
    {
        public string Header { get; } = "Welcome";

        #region Services

        private readonly IAppService _appService;
        //private readonly IRegionManager _regionManager;
        private readonly IRepository<XmlDataProvider> _repositoryXml;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IEventAggregator _eventAggregator;

        #endregion

        #region Commands

        public RelayCommand CloseApplication { get; private set; }
        public RelayCommand<object> OpenProject { get; private set; }
        public RelayCommand CreateProject { get; private set; }

        #endregion

        public WelcomeViewModel(IAppService appService, IRepository<XmlDataProvider> repositoryXml, /*IRegionManager regionManager,*/ IDialogCoordinator dialogCoordinator, IEventAggregator eventAggregator)
        {
            _appService = appService;
            //_regionManager = regionManager;
            _dialogCoordinator = dialogCoordinator;
            _eventAggregator = eventAggregator;
            _repositoryXml = repositoryXml;
            CloseApplication = new RelayCommand(() => _appService.CloseApp());
            OpenProject = new RelayCommand<object>(p =>
            {
                var x = p == null ? _repositoryXml.LoadData() : _repositoryXml.LoadData(p.ToString());
                if (x != null)
                {
                    _appService.InitilizeBaseModules();
                    _eventAggregator.GetEvent<OpenProjectEvent>().Publish(_repositoryXml.CurrentPath);
                    //foreach (var item in _regionManager.Regions[Names.MainContentRegion].Views)
                    //    _regionManager.Regions[Names.MainContentRegion].Deactivate(item);
                }
                else
                    _dialogCoordinator.ShowMessageAsync(this, "Ошибка", "Указанная папка не соответствует необходимым требованиям.");
            });
            CreateProject = new RelayCommand(() => 
            {
                var path = _repositoryXml.CreateData();

                if (path == "error")
                    _dialogCoordinator.ShowMessageAsync(this, "Ошибка", "В указанной папке уже содержиться проект. Нельзя перезаписывать существующие проекты.");
                else if (path != null)
                    OpenProject.Execute(path);
            });
            _eventAggregator.GetEvent<OpenLink>().Subscribe(p => OpenProject.Execute(p));
        }

    }
}
