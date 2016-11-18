namespace FOMOD.Creator.ViewModels
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Windows.Input;
    using FOMOD.Creator.Commands;
    using FOMOD.Creator.Domain.Models;
    using FOMOD.Creator.Interfaces;
    using FOMOD.Creator.PrismEvent;
    using Prism.Events;
    using PropertyChanged;

    [ImplementPropertyChanged]
    public class RecentViewModel
    {
        private static readonly string FileName = ".creator";
        private static readonly string SettingsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".fomod");
        private static readonly string SettingFilePath = Path.Combine(SettingsFolder, FileName);


        private readonly IDataService _dataService;
        private readonly IEventAggregator _eventAggregator;

        private ICommand _goToCommand;
        private ICommand _removeCommand;

        public RecentViewModel(IEventAggregator eventAggregator, IDataService dataService)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;
            _eventAggregator.GetEvent<OpenProjectEvent>().Subscribe(UpdateProjectList);
            ProjectLinkList = ReadProjectLinkListFile();
        }

        public ICommand GoToCommand
        {
            get
            {
                return _goToCommand ?? (_goToCommand = new RelayCommand<string>(_eventAggregator.GetEvent<OpenLink>().Publish));
            }
        }

        public ProjectLinkList ProjectLinkList { get; }

        public ICommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new RelayCommand<ProjectLink>(RemoveRecentProject));
            }
        }

        private ProjectLinkList ReadProjectLinkListFile()
        {
            if (!File.Exists(SettingFilePath))
                return new ProjectLinkList();
            return _dataService.LoadData<ProjectLinkList>(SettingFilePath);
        }

        private void RemoveRecentProject(ProjectLink link)
        {
            if (link == null)
                return;
            ProjectLinkList.Links.Remove(link);
            SaveProjectLinkListFile();
        }

        private void SaveProjectLinkListFile()
        {
            Directory.CreateDirectory(SettingsFolder);
            _dataService.SaveData(ProjectLinkList, SettingFilePath);
        }

        private void UpdateProjectList(Project p)
        {
            var item = ProjectLinkList.Links.FirstOrDefault(i => i.FolderPath == p.Source);
            if (item == null)
                ProjectLinkList.Links.Add(new ProjectLink(p.Info.Name, p.Source));
            else
                item.ProjectName = p.Info.Name;
            SaveProjectLinkListFile();
        }
    }
}
