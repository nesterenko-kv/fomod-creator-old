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
        public WelcomeViewModel(IAppService appService, IDialogCoordinator dialogCoordinator,
            IEventAggregator eventAggregator, IServiceLocator serviceLocator)
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
                {
                    if (_repository.RepositoryStatus == RepositoryStatus.CantSelectFolder)
                    {
                        _dialogCoordinator.ShowMessageAsync(this, "Ошибка", "Указанная папка не соответствует необходимым требованиям.");
                    }
                    else if (_repository.RepositoryStatus == RepositoryStatus.Error)
                    {
                        _dialogCoordinator.ShowMessageAsync(this, "Ошибка",  "Произошла ошибка при загрузки проекта - обратитесь к разработчику");
                    }
                    else if (_repository.RepositoryStatus == RepositoryStatus.Cancel)
                    {
                        //_dialogCoordinator.ShowMessageAsync(this, "Отмена", "Отмена");
                    }
                    else
                    {
                        throw new ApplicationException();
                    }
                }
            });
            CreateProject = new RelayCommand(() => 
            {
                var _repository = _serviceLocator.GetInstance<IRepository<ProjectRoot>>();
                var path = _repository.CreateData();
                if (_repository.RepositoryStatus == RepositoryStatus.FolderIsAlreadyUse)
                    _dialogCoordinator.ShowMessageAsync(this, "Ошибка", "Нельзя использовать папку в которой существуют файлы проекта");
                else if (_repository.RepositoryStatus == RepositoryStatus.Ok)
                    OpenProject.Execute(path);
                else if (_repository.RepositoryStatus == RepositoryStatus.Cancel)
                { }
                else
                    throw new ApplicationException();
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