using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary.Commands;
using Prism.Events;
using Module.Welcome.PrismEvent;
using FomodModel.Base;
using MahApps.Metro.Controls.Dialogs;

namespace Module.Welcome.ViewModel
{
    public class WelcomeViewModel
    {
        public string Header { get; } = "Welcome";

        #region Services

        private readonly IAppService _appService;
        private readonly IRepository<ProjectRoot> _repository;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IEventAggregator _eventAggregator;

        #endregion

        #region Commands

        public RelayCommand CloseApplication { get; private set; }
        public RelayCommand<string> OpenProject { get; private set; }
        public RelayCommand CreateProject { get; private set; }

        #endregion

        public WelcomeViewModel(IAppService appService, IRepository<ProjectRoot> repository, IDialogCoordinator dialogCoordinator, IEventAggregator eventAggregator)
        {
            _appService = appService;
            //_regionManager = regionManager;
            _dialogCoordinator = dialogCoordinator;
            _eventAggregator = eventAggregator;
            _repository = repository;
            CloseApplication = new RelayCommand(() => _appService.CloseApp());
            OpenProject = new RelayCommand<string>(p =>
            {
                var x = _repository.LoadData(p);
                if (x != null)
                {
                    _appService.InitilizeBaseModules();
                    _eventAggregator.GetEvent<OpenProjectEvent>().Publish(_repository.CurrentPath);
                }
                else
                    _dialogCoordinator.ShowMessageAsync(this, "Ошибка", "Указанная папка не соответствует необходимым требованиям.");
            });
            CreateProject = new RelayCommand(() => { });
            _eventAggregator.GetEvent<OpenLink>().Subscribe(p => OpenProject.Execute(p));
        }

    }
}
