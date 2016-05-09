using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary.Commands;
//using Prism.Regions;
using Prism.Events;
using Module.Welcome.PrismEvent;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Data;
using Prism.Regions;
using FomodInfrastructure;
using Microsoft.Practices.ServiceLocation;

namespace Module.Welcome.ViewModel
{
    public class WelcomeViewModel
    {
        public string Header { get; } = "Welcome";

        #region Services

        private readonly IAppService _appService;
        private readonly IRegionManager _regionManager;
        private readonly IRepository<XmlDataProvider> _repositoryXml;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IEventAggregator _eventAggregator;
        private readonly IServiceLocator _serviceLocator;

        #endregion

        #region Commands

        public RelayCommand CloseApplication { get; private set; }
        public RelayCommand<object> OpenProject { get; private set; }
        public RelayCommand CreateProject { get; private set; }

        #endregion

        public WelcomeViewModel(IAppService appService, 
            IRepository<XmlDataProvider> repositoryXml, 
            IRegionManager regionManager, 
            IDialogCoordinator dialogCoordinator, 
            IEventAggregator eventAggregator,
            IServiceLocator serviceLocator)
        {
            _appService = appService;
            _regionManager = regionManager;
            _dialogCoordinator = dialogCoordinator;
            _eventAggregator = eventAggregator;
            _repositoryXml = repositoryXml;
            _serviceLocator = serviceLocator;
            CloseApplication = new RelayCommand(() => _appService.CloseApp());
            OpenProject = new RelayCommand<object>(p =>
            {
                var rep = _serviceLocator.GetInstance<IRepository<XmlDataProvider>>();
                var x = p == null ? rep.LoadData() : rep.LoadData(p.ToString());
                if (x != null)
                {
                    //_appService.InitilizeBaseModules();

                    (_appService as dynamic).CreateEditorModule(rep);


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
