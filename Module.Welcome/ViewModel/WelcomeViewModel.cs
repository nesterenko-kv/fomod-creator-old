using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.ServiceLocation;
using Module.Welcome.PrismEvent;
using Prism.Events;
using System;

namespace Module.Welcome.ViewModel
{
    public class WelcomeViewModel
    {
        public WelcomeViewModel(IAppService appService, IDialogCoordinator dialogCoordinator, IEventAggregator eventAggregator, IServiceLocator serviceLocator)
        {
            _appService = appService;
            _dialogCoordinator = dialogCoordinator;
            _eventAggregator = eventAggregator;
            _serviceLocator = serviceLocator;

            CloseApplication = new RelayCommand(() => _appService.CloseApp());
            OpenProject = new RelayCommand<string>(p =>
            {
                var repository = _serviceLocator.GetInstance<IRepository<ProjectRoot>>();
                var x = repository.LoadData(p);
                if (x == null)
                {
                    switch (repository.RepositoryStatus)
                    {
                        case RepositoryStatus.CantSelectFolder:
                            _dialogCoordinator.ShowMessageAsync(this, "Ошибка",
                                "Указанная папка не соответствует необходимым требованиям.");
                            break;
                        case RepositoryStatus.Error:
                            _dialogCoordinator.ShowMessageAsync(this, "Ошибка",
                                "Произошла ошибка при загрузки проекта - обратитесь к разработчику");
                            break;
                        case RepositoryStatus.Cancel:
                            //_dialogCoordinator.ShowMessageAsync(this, "Отмена", "Отмена");
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
                    _eventAggregator.GetEvent<OpenProjectEvent>().Publish(repository.CurrentPath);
                }
            });
            CreateProject = new RelayCommand(() => 
            {
                var repository = _serviceLocator.GetInstance<IRepository<ProjectRoot>>();
                var path = repository.CreateData();
                switch (repository.RepositoryStatus)
                {
                    case RepositoryStatus.FolderIsAlreadyUse:
                        _dialogCoordinator.ShowMessageAsync(this, "Ошибка",
                            "Нельзя использовать папку в которой существуют файлы проекта");
                        break;
                    case RepositoryStatus.Ok:
                        OpenProject.Execute(path);
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
            });
            _eventAggregator.GetEvent<OpenLink>().Subscribe(p => OpenProject.Execute(p));
        }

        public string Header { get; } = "Welcome";

        #region Services

        private readonly IAppService _appService;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IEventAggregator _eventAggregator;
        private readonly IServiceLocator _serviceLocator;

        #endregion

        #region Commands

        public RelayCommand CloseApplication { get; private set; }
        public RelayCommand<string> OpenProject { get; }
        public RelayCommand CreateProject { get; private set; }

        #endregion
    }
}