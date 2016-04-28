using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary.Commands;
using Module.Welcome.Model;
using Module.Welcome.PrismEvent;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace Module.Welcome.ViewModel
{
    public class LastProjectsViewModel: BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService _dataService;

        private readonly string _basePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        private const string SubPath = @"\FOMODplist.xml";

        public ProjectLinkList ProjectLinkList { get; set; } = new ProjectLinkList();

        private ICommand _goTo;


        public LastProjectsViewModel(IEventAggregator eventAggregator, IDataService dataService)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;


            var list = ReadProjectLinkListFile();
            if (list!=null)
            {
                ProjectLinkList = list;
                OnPropertyChanged(nameof(ProjectLinkList));
            }

            
            _eventAggregator.GetEvent<OpenProjectEvent>().Subscribe( p =>
            {
               var project = ProjectLinkList.Links.FirstOrDefault(i => i.FolderPath == p);
                if (project != null) return;
                ProjectLinkList.Links.Add(new ProjectLinkModel{FolderPath = p});
                SaveProjectLinkListFile();
            });
        }

        public ICommand GoTo
        {
            get
            {
                if (_goTo == null)
                    _goTo = new RelayCommand(p => _eventAggregator.GetEvent<OpenLink>().Publish(p.ToString()));
                return _goTo;
            }
        }


        private ProjectLinkList ReadProjectLinkListFile()
        {
            if (File.Exists(_basePath + SubPath))
            {
                try
                {
                    return _dataService.DeserializeObject<ProjectLinkList>(_basePath + SubPath);
                }
                catch (Exception)
                {
                    throw; //TODO обработать ошибки
                }
            }

            return null;
        }
        private bool SaveProjectLinkListFile()
        {
            if (Directory.Exists(_basePath))
            {
                try
                {
                    _dataService.SerializeObject(ProjectLinkList, _basePath + SubPath);
                    return true;
                }
                catch (Exception)
                {
                    throw; //TODO обработать ошибки
                }
            }

            return false;
        }

    }
}
