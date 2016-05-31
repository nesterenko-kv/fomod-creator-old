using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.AppModel;
using FomodModel.Base;
using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WelcomeNew
{
    public class WelcomeNewViewModel
    {
        public string Header { get; set; } = "Главная";

        #region Services

        private readonly IDataService _dataService;
        private readonly IServiceLocator _serviceLocator;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILogger _logger;

        #endregion

        public ObservableCollection<ProjectLinkModel> Links { get; }
        private const string SubPath = @"\FOMOD_LINKS.xml";
        private readonly string _basePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
       


        #region Commands

        public ICommand RemoveLinkCommand { get; private set; }
        public ICommand OpenProjectCommand { get; private set; }

        #endregion


        public WelcomeNewViewModel(IDataService dataService, IServiceLocator serviceLocator, IEventAggregator eventAggregator, ILogger logger)
        {
            _dataService = dataService;
            _serviceLocator = serviceLocator;
            _eventAggregator = eventAggregator;
            _logger = logger;

            Links = LoadLinks();
            
            OpenProjectCommand = new RelayCommand<string>(OpenProject);
        }




        #region Methods


        private void OpenProject(string path)
        {
            var repa = _serviceLocator.GetInstance<IRepository<ProjectRoot>>();
            var dataObj = repa.LoadData(path);
            if (dataObj != null)
            {
                var projectLinkModel = Links.FirstOrDefault(i => i.FolderPath == dataObj.FolderPath);
                if (projectLinkModel == null)
                    Links.Add(new ProjectLinkModel { FolderPath = dataObj.FolderPath, ProjectName = dataObj.ModuleInformation.Name });
                else
                    projectLinkModel.ProjectName = dataObj.ModuleInformation.Name;

                SaveLinks();
                _logger.Log($"[Open Project Publish Event] " + dataObj.FolderPath);
                _eventAggregator.GetEvent<PubSubEvent<IRepository<ProjectRoot>>>().Publish(repa);

            }
        }

        /// <summary>
        /// Удаляет проект из списка ранее открытых проектов
        /// </summary>
        /// <param name="p">объект данных хранящий сведения о ранее открытом проекте</param>
        private void RemoveRecentProject(ProjectLinkModel p)
        {
            if (p == null) return;
            Links.Remove(p);
            SaveLinks();
        }


        /// <summary>
        /// Загружает данные из файла
        /// </summary>
        /// <returns>Возращает null если файл не найден</returns>
        private ObservableCollection<ProjectLinkModel> LoadLinks()
        {
            if (!File.Exists(_basePath + SubPath)) return new ObservableCollection<ProjectLinkModel>();
            var linksObj = _dataService.DeserializeObject<ObservableCollection<ProjectLinkModel>>(_basePath + SubPath);
            foreach (var item in linksObj.Where(item => string.IsNullOrWhiteSpace(item.FolderPath)))
                linksObj.Remove(item);
            return linksObj;
        }

        /// <summary>
        /// Сохраняет список последних открытых проектов
        /// </summary>
        private void SaveLinks()
        {
            if (Directory.Exists(_basePath))
                _dataService.SerializeObject(Links, _basePath + SubPath);
        }

        #endregion
    }
}
