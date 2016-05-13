using System;
using System.IO;
using System.Linq;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary.Commands;
using Module.Welcome.Model;
using Module.Welcome.PrismEvent;
using Prism.Events;
using Prism.Mvvm;

namespace Module.Welcome.ViewModel
{
    public class LastProjectsViewModel : BindableBase
    {
        private const string SubPath = @"\FOMODplist.xml";
        private readonly string _basePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        #region Services

        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService _dataService;

        #endregion

        #region Commands

        public RelayCommand<string> GoToCommand { get; private set; }
        public RelayCommand<ProjectLinkModel> RemoveCommand { get; private set; }

        #endregion
        
        #region Properties

        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public ProjectLinkList ProjectLinkList { get; set; } = new ProjectLinkList();

        #endregion

        public LastProjectsViewModel(IEventAggregator eventAggregator, IDataService dataService)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;
            GoToCommand = new RelayCommand<string>(_eventAggregator.GetEvent<OpenLink>().Publish);
            RemoveCommand = new RelayCommand<ProjectLinkModel>(RemoveRecentProject);
            _eventAggregator.GetEvent<OpenProjectEvent>().Subscribe(AddProjectInListAfterOpen);
            var list = ReadProjectLinkListFile();
            if (list != null)
                ProjectLinkList = list;
        }
        
        #region Methods

        private void RemoveRecentProject(ProjectLinkModel p)
        {
            if (p == null) return;
            ProjectLinkList.Links.Remove(p);
            SaveProjectLinkListFile();
        }

        private void AddProjectInListAfterOpen(string p)
        {
            if (ProjectLinkList.Links.FirstOrDefault(i => i.FolderPath == p) != null) return;
            ProjectLinkList.Links.Add(new ProjectLinkModel { FolderPath = p });
            SaveProjectLinkListFile();
        }

        private ProjectLinkList ReadProjectLinkListFile()
        {
            if (!File.Exists(_basePath + SubPath)) return null;
            var link = _dataService.DeserializeObject<ProjectLinkList>(_basePath + SubPath);
            foreach (var item in link.Links.Where(item => string.IsNullOrWhiteSpace(item.FolderPath)))
                link.Links.Remove(item);
            return link;
        }

        private void SaveProjectLinkListFile()
        {
            if (Directory.Exists(_basePath))
                _dataService.SerializeObject(ProjectLinkList, _basePath + SubPath);
        }
        
        #endregion
    }
}