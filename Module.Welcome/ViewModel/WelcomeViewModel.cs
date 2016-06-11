using FomodInfrastructure.Interfaces;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.ServiceLocation;
using Module.Welcome.PrismEvent;
using Prism.Events;

namespace Module.Welcome.ViewModel
{
    public class WelcomeViewModel: ProjectWorkerBaseViewModel
    {
        public WelcomeViewModel(IEventAggregator eventAggregator, IDialogCoordinator dialogCoordinator, IServiceLocator serviceLocator, IAppService appService, IFolderBrowserDialog folderBrowserDialog)
            : base(eventAggregator, dialogCoordinator, serviceLocator, appService, folderBrowserDialog)
        {
            eventAggregator.GetEvent<OpenLink>().Subscribe(OpenProject);
        }
        
        public string Header { get; } = @"Welcome";
        
    }
}