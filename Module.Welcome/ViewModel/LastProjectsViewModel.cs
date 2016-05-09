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

        public LastProjectsViewModel(IEventAggregator eventAggregator, IDataService dataService)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;
            GoTo = new RelayCommand<string>(p => _eventAggregator.GetEvent<OpenLink>().Publish(p));
            var list = ReadProjectLinkListFile();
            if (list != null)
                ProjectLinkList = list;
            _eventAggregator.GetEvent<OpenProjectEvent>().Subscribe(p =>
            {
                if (ProjectLinkList.Links.FirstOrDefault(i => i.FolderPath == p) != null) return;
                ProjectLinkList.Links.Add(new ProjectLinkModel {FolderPath = p});
                SaveProjectLinkListFile();
            });
        }

        #region Properties

        [Aspect(typeof (AspectINotifyPropertyChanged))]
        public ProjectLinkList ProjectLinkList { get; set; } = new ProjectLinkList();

        #endregion

        #region Commands

        public RelayCommand<string> GoTo { get; private set; }

        #endregion

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

        #region Services

        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService _dataService;

        #endregion
    }
}