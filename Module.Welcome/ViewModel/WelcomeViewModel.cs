using System;
using System.Windows.Input;
using FomodInfrastructure;
using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.ServiceLocation;
using Module.Welcome.PrismEvent;
using Prism.Events;

namespace Module.Welcome.ViewModel
{
    public class WelcomeViewModel
    {
        public WelcomeViewModel(IAppService appService, IDialogCoordinator dialogCoordinator, IEventAggregator eventAggregator, IServiceLocator serviceLocator)
        {
            _appService = appService;
            _dialogCoordinator = dialogCoordinator;
            _serviceLocator = serviceLocator;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenLink>().Subscribe(OpenProject);
        }

        public string Header { get; } = "Welcome";

        #region Services

        private readonly IAppService _appService;

        private readonly IDialogCoordinator _dialogCoordinator;

        private readonly IEventAggregator _eventAggregator;

        private readonly IServiceLocator _serviceLocator;

        #endregion

        #region Commands

        private ICommand _closeApplicationCommand;

        public ICommand CloseApplicationCommand
        {
            get { return _closeApplicationCommand ?? (_closeApplicationCommand = new RelayCommand(_appService.CloseApp)); }
        }

        private ICommand _createProjectCommand;

        public ICommand CreateProjectCommand
        {
            get { return _createProjectCommand ?? (_createProjectCommand = new RelayCommand(CreateProject)); }
        }

        private ICommand _openProjectCommand;

        public ICommand OpenProjectCommand
        {
            get { return _openProjectCommand ?? (_openProjectCommand = new RelayCommand<string>(OpenProject)); }
        }

        #endregion

        #region Methods

        private async void CreateProject()
        {
            var repository = _serviceLocator.GetInstance<IRepository<ProjectRoot>>();
            var path = repository.CreateData();
            switch (repository.RepositoryStatus)
            {
                case RepositoryStatus.FolderIsAlreadyUse:
                    await _dialogCoordinator.ShowMessageAsync(this, "Ошибка", "Нельзя использовать папку в которой существуют файлы проекта");
                    break;
                case RepositoryStatus.Ok:
                    OpenProject(path);
                    break;
                case RepositoryStatus.Cancel:
                    break;
                case RepositoryStatus.None:
                    break;
                case RepositoryStatus.Error:
                    break;
                case RepositoryStatus.CantSelectFolder:
                    break;
                default:
                    throw new ApplicationException();
            }
        }

        private async void OpenProject(string p)
        {
            var repository = _serviceLocator.GetInstance<IRepository<ProjectRoot>>();
            var x = repository.LoadData(p);
            if (x == null)
            {
                switch (repository.RepositoryStatus)
                {
                    case RepositoryStatus.CantSelectFolder:
                        await _dialogCoordinator.ShowMessageAsync(this, "Ошибка", "Указанная папка не соответствует необходимым требованиям.");
                        break;
                    case RepositoryStatus.Error:
                        await _dialogCoordinator.ShowMessageAsync(this, "Ошибка", "Произошла ошибка при загрузки проекта - обратитесь к разработчику");
                        break;
                    case RepositoryStatus.Cancel:
                        break;
                    case RepositoryStatus.None:
                        break;
                    case RepositoryStatus.Ok:
                        break;
                    case RepositoryStatus.FolderIsAlreadyUse:
                        break;
                    default:
                        throw new ApplicationException();
                }
            }
            else
            {
                _appService.CreateEditorModule(repository);
                _eventAggregator.GetEvent<OpenProjectEvent>().Publish(x);
            }
        }

        #endregion
    }
}