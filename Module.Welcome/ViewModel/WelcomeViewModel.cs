using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary.Commands;
using Prism.Events;
using Module.Welcome.PrismEvent;
using FomodModel.Base;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.ServiceLocation;

namespace Module.Welcome.ViewModel
{
    public class WelcomeViewModel
    {
        public string Header { get; } = "Welcome";

        #region Services

        private readonly IAppService _appService;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IEventAggregator _eventAggregator;
        private readonly IServiceLocator _serviceLocator;

        #endregion

        #region Commands

        public RelayCommand CloseApplication { get; private set; }
        public RelayCommand<string> OpenProject { get; private set; }
        public RelayCommand CreateProject { get; private set; }

        #endregion

        public WelcomeViewModel(IAppService appService, IDialogCoordinator dialogCoordinator, IEventAggregator eventAggregator, IServiceLocator serviceLocator)
        {
            _appService = appService;
            _dialogCoordinator = dialogCoordinator;
            _eventAggregator = eventAggregator;
            _serviceLocator = serviceLocator;

            CloseApplication = new RelayCommand(() => _appService.CloseApp());
            OpenProject = new RelayCommand<string>(p =>
            {
                var _repository = _serviceLocator.GetInstance<IRepository<ProjectRoot>>();
                var x = _repository.LoadData(p);
                if (x != null)
                {
                    _appService.CreateEditorModule(_repository);
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
