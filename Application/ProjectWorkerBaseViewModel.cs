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

namespace MainApplication
{
    public abstract class ProjectWorkerBaseViewModel
    {
        protected ProjectWorkerBaseViewModel(IEventAggregator eventAggregator, IDialogCoordinator dialogCoordinator, IServiceLocator serviceLocator, IAppService appService)
        {
            EventAggregator = eventAggregator;
            DialogCoordinator = dialogCoordinator;
            ServiceLocator = serviceLocator;
            AppService = appService;
        }

        #region Services

        protected readonly IEventAggregator EventAggregator;

        protected readonly IDialogCoordinator DialogCoordinator;

        protected readonly IServiceLocator ServiceLocator;

        protected readonly IAppService AppService;

        #endregion

        #region Methods

        private async void CreateProject()
        {
            var repository = ServiceLocator.GetInstance<IRepository<ProjectRoot>>();
            var path = repository.CreateData();
            switch (repository.RepositoryStatus)
            {
                case RepositoryStatus.FolderIsAlreadyUsed:
                    await DialogCoordinator.ShowMessageAsync(this, "Error", "Folder already used."); //TODO: Localize
                    break;
                case RepositoryStatus.Ok:
                    OpenProject(path);
                    break;
                case RepositoryStatus.Cancel:
                    break;
                case RepositoryStatus.None:
                    break;
                case RepositoryStatus.Error:
                    await DialogCoordinator.ShowMessageAsync(this, "Error", "An error occurred while creating the project."); //TODO: Localize
                    break;
                case RepositoryStatus.CantUseFolder:
                    await DialogCoordinator.ShowMessageAsync(this, "Error", "The specified folder doesn't correspond to necessary requirements."); //TODO: Localize
                    break;
                default:
                    throw new ApplicationException();
            }
        }

        private async void OpenProject(string p)
        {
            var repository = ServiceLocator.GetInstance<IRepository<ProjectRoot>>();
            var x = repository.LoadData(p);
            if (x == null)
            {
                switch (repository.RepositoryStatus)
                {
                    case RepositoryStatus.CantUseFolder:
                        await DialogCoordinator.ShowMessageAsync(this, "Error", "The specified folder doesn't correspond to necessary requirements."); //TODO: Localize
                        break;
                    case RepositoryStatus.Error:
                        await DialogCoordinator.ShowMessageAsync(this, "Error", "An error occured while loading the project folder."); //TODO: Localize
                        break;
                    case RepositoryStatus.Cancel:
                        break;
                    case RepositoryStatus.None:
                        break;
                    case RepositoryStatus.Ok:
                        break;
                    case RepositoryStatus.FolderIsAlreadyUsed:
                        break;
                    default:
                        throw new ApplicationException();
                }
            }
            else
            {
                AppService.CreateEditorModule(repository);
                EventAggregator.GetEvent<OpenProjectEvent>().Publish(x);
            }
        }

        #endregion

        #region Commands

        private ICommand _createProjectCommand;

        public ICommand CreateProjectCommand
        {
            get { return _createProjectCommand ?? (_createProjectCommand = new RelayCommand(CreateProject)); }
        }

        private ICommand _closeApplicationCommand;

        public ICommand CloseApplicationCommand
        {
            get { return _closeApplicationCommand ?? (_closeApplicationCommand = new RelayCommand(AppService.CloseApp)); }
        }

        private ICommand _openProjectCommand;

        public ICommand OpenProjectCommand
        {
            get { return _openProjectCommand ?? (_openProjectCommand = new RelayCommand<string>(OpenProject)); }
        }

        #endregion
    }
}