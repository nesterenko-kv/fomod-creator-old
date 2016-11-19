namespace FOMOD.Creator.ViewModels
{
    using System.Linq;
    using System.Windows.Input;
    using FOMOD.Creator.Commands;
    using FOMOD.Creator.Domain.Models;
    using FOMOD.Creator.PrismEvent;
    using Prism.Events;
    using PropertyChanged;

    [ImplementPropertyChanged]
    public class RecentViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        private ICommand _goToCommand;
        private ICommand _removeCommand;

        public RecentViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenProjectEvent>().Subscribe(UpdateProjectList);
        }

        public ICommand GoToCommand
        {
            get
            {
                return _goToCommand ?? (_goToCommand = new RelayCommand<string>(_eventAggregator.GetEvent<OpenLink>().Publish));
            }
        }

        public ProjectLinkList ProjectLinkList
        {
            get
            {
                return Properties.Settings.Default.RecentDocuments ?? (Properties.Settings.Default.RecentDocuments = new ProjectLinkList());
            }
        }

        public ICommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new RelayCommand<ProjectLink>(RemoveRecentProject));
            }
        }
        
        private void RemoveRecentProject(ProjectLink link)
        {
            if (link == null)
                return;
            ProjectLinkList.Links.Remove(link);
            Properties.Settings.Default.Save();
        }
        
        private void UpdateProjectList(Project p)
        {
            var item = ProjectLinkList.Links.FirstOrDefault(i => i.FolderPath == p.Source);
            if (item == null)
                ProjectLinkList.Links.Add(new ProjectLink(p.Info.Name, p.Source));
            else
                item.ProjectName = p.Info.Name;
            Properties.Settings.Default.Save();
        }
    }
}
