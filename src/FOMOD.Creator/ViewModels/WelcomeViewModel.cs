namespace FOMOD.Creator.ViewModels
{
    using FOMOD.Creator.Interfaces;
    using FOMOD.Creator.PrismEvent;
    using MahApps.Metro.Controls.Dialogs;
    using Prism.Events;
    using Prism.Regions;
    using StructureMap;

    public class WelcomeViewModel : ProjectWorkerBaseViewModel, IHaveDisplayName
    {
        public WelcomeViewModel(IEventAggregator eventAggregator, IDialogCoordinator dialogCoordinator, IContainer container, IRegionManager regionManager)
            : base(dialogCoordinator, container, eventAggregator, regionManager)
        {
            eventAggregator.GetEvent<OpenLink>().Subscribe(OpenProject);
        }

        public string DisplayName
        {
            get
            {
                return "Welcome";
            }
        }
    }
}
