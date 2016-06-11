using System.Windows.Input;
using FomodInfrastructure.Interfaces;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.ServiceLocation;
using Module.Welcome.PrismEvent;
using Prism.Events;

namespace Module.Welcome.ViewModel
{
    public abstract class ProjectWorkerBaseViewModel
    {
        protected ProjectWorkerBaseViewModel(IEventAggregator eventAggregator, IDialogCoordinator dialogCoordinator, IServiceLocator serviceLocator, IAppService appService, IFolderBrowserDialog folderBrowserDialog)
        {
            EventAggregator = eventAggregator;
            DialogCoordinator = dialogCoordinator;
            ServiceLocator = serviceLocator;
            AppService = appService;
            FolderBrowserDialog = folderBrowserDialog;
        }

        #region Services

        protected readonly IEventAggregator EventAggregator;

        protected readonly IDialogCoordinator DialogCoordinator;

        protected readonly IServiceLocator ServiceLocator;

        protected readonly IFolderBrowserDialog FolderBrowserDialog;

        protected readonly IAppService AppService;

        #endregion

        #region Methods

        private bool TryGetFolderPath(out string path)
        {
            FolderBrowserDialog.Reset();
            FolderBrowserDialog.CheckFolderExists = true;
            var result = FolderBrowserDialog.ShowDialog();
            path = FolderBrowserDialog.SelectedPath;
            return result;
        }

        public void CreateProject()
        {
            var repository = ServiceLocator.GetInstance<IRepository<ProjectRoot>>();
            string path;
            if (!TryGetFolderPath(out path))
                return;
            var result = repository.Create(path);
            if (!result.Success)
                return;
            AppService.CreateEditorModule(repository);
            EventAggregator.GetEvent<OpenProjectEvent>().Publish(result.Data);
        }

        public void OpenProject(string path = null)
        {
            var repository = ServiceLocator.GetInstance<IRepository<ProjectRoot>>();
            if (string.IsNullOrEmpty(path))
                if (!TryGetFolderPath(out path))
                    return;
            var result = repository.Load(path);
            if (!result.Success)
                return;
            AppService.CreateEditorModule(repository);
            EventAggregator.GetEvent<OpenProjectEvent>().Publish(result.Data);
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