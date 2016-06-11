using System;
using System.IO;
using System.Linq;
using System.Windows.Input;
using AspectInjector.Broker;
using FomodInfrastructure.Aspects;
using FomodInfrastructure.Interfaces;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.AppModel;
using FomodModel.Base;
using Module.Welcome.PrismEvent;
using Prism.Events;

namespace Module.Welcome.ViewModel
{
    public class LastProjectsViewModel
    {
        private const string SubPath = @"\FOMODplist.xml";

        private readonly string _personalPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public LastProjectsViewModel(IEventAggregator eventAggregator, IDataService dataService)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;
            _eventAggregator.GetEvent<OpenProjectEvent>().Subscribe(UpdateProjectList);
            ProjectLinkList = ReadProjectLinkListFile() ?? new ProjectLinkList();
        }

        #region Properties

        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public ProjectLinkList ProjectLinkList { get; }

        #endregion

        #region Services

        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService _dataService;

        #endregion

        #region Commands

        private ICommand _goToCommand;

        public ICommand GoToCommand
        {
            get { return _goToCommand ?? (_goToCommand = new RelayCommand<string>(_eventAggregator.GetEvent<OpenLink>().Publish)); }
        }

        private ICommand _removeCommand;

        public ICommand RemoveCommand
        {
            get { return _removeCommand ?? (_removeCommand = new RelayCommand<ProjectLinkModel>(RemoveRecentProject)); }
        }

        #endregion

        #region Methods

        private void RemoveRecentProject(ProjectLinkModel p)
        {
            if (p == null)
                return;
            ProjectLinkList.Links.Remove(p);
            SaveProjectLinkListFile();
        }

        private void UpdateProjectList(ProjectRoot p)
        {
            var item = ProjectLinkList.Links.FirstOrDefault(i => i.FolderPath == p.DataSource);
            if (item == null)
                ProjectLinkList.Links.Add(ProjectLinkModel.Create(p.ModuleInformation.Name, p.DataSource));
            else
                item.ProjectName = p.ModuleInformation.Name;
            SaveProjectLinkListFile();
        }

        private ProjectLinkList ReadProjectLinkListFile()
        {
            if (!File.Exists(_personalPath + SubPath))
                return null;
            var link = _dataService.LoadData<ProjectLinkList>(_personalPath + SubPath);
            foreach (var item in link.Links.Where(item => string.IsNullOrWhiteSpace(item.FolderPath)))
                link.Links.Remove(item);
            return link;
        }

        private void SaveProjectLinkListFile()
        {
            if (Directory.Exists(_personalPath))
                _dataService.SaveData(ProjectLinkList, _personalPath + SubPath);
        }

        #endregion
    }
}