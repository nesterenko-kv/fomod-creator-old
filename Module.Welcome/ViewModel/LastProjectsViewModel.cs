using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary.Commands;
using Module.Welcome.Model;
using Module.Welcome.PrismEvent;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.IO;
using System.Linq;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace Module.Welcome.ViewModel
{
    public class LastProjectsViewModel: BindableBase
    {
        private readonly string _basePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        private const string SubPath = @"\FOMODplist.xml";

        #region Services

        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService _dataService;

        #endregion

        #region Properties

        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public ProjectLinkList ProjectLinkList { get; set; } = new ProjectLinkList();

        #endregion

        #region Commands

        public RelayCommand<object> GoTo { get; private set; }

        #endregion

        public LastProjectsViewModel(IEventAggregator eventAggregator, IDataService dataService)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;
            GoTo = new RelayCommand<object>(p => _eventAggregator.GetEvent<OpenLink>().Publish(p.ToString()));
            var list = ReadProjectLinkListFile();
            if (list != null)
                ProjectLinkList = list;
            _eventAggregator.GetEvent<OpenProjectEvent>().Subscribe(p =>
            {
                var project = ProjectLinkList.Links.FirstOrDefault(i => i.FolderPath == p);
                if (project != null) return;
                ProjectLinkList.Links.Add(new ProjectLinkModel {FolderPath = p});
                SaveProjectLinkListFile();
            });
        }

        private ProjectLinkList ReadProjectLinkListFile()
        {
            if (File.Exists(_basePath + SubPath))
                return _dataService.DeserializeObject<ProjectLinkList>(_basePath + SubPath);
            return null;
        }

        private void SaveProjectLinkListFile()
        {
            if (Directory.Exists(_basePath))
                _dataService.SerializeObject(ProjectLinkList, _basePath + SubPath);
        }
    }
}
