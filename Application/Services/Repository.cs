using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using FomodInfrastructure.Interface;
using FomodModel.Base;
using Microsoft.Practices.ServiceLocation;
using Prism.Logging;

namespace MainApplication.Services
{
    public class Repository : IRepository<ProjectRoot>
    {
        #region Services

        private readonly IDataService _dataService;
        private readonly IFolderBrowserDialog _folderBrowserDialog;
        private readonly ILogger _logger;
        private readonly IServiceLocator _serviceLocator;

        #endregion

        #region IRepository

        public ProjectRoot LoadData(string path = null) => _projectRoot = path != null ? LoadProjectFromPath(path) : LoadProjectIfPathNull();

        public bool SaveData(string path = null) => path != null ? SaveProjectFromPath(path) : SaveProjectIfPathNull();

        public ProjectRoot GetData() => _projectRoot;

        public string CurrentPath
        {
            get { return _projectRoot.FolderPath; }
            set { _projectRoot.FolderPath = value; }
        }

        public string CreateData()
        {
            _folderBrowserDialog.ShowDialog();
            var path = _folderBrowserDialog.SelectedPath;
            _folderBrowserDialog.Reset();
            if (!string.IsNullOrWhiteSpace(path))
            {
                if (CheckFiles(path))
                {
                    RepositoryStatus = RepositoryStatus.FolderIsAlreadyUse;
                    return null;
                }

                Directory.CreateDirectory(path + @"\fomod");
                Directory.CreateDirectory(path + @"\Data");
                
                if (!File.Exists(path + InfoSubPath))
                    GetXElementResource(InfoResourcePath).Save(path + InfoSubPath);
                if (!File.Exists(path + ConfigurationSubPath))
                    GetXElementResource(ConfigResourcePath).Save(path + ConfigurationSubPath);

                RepositoryStatus = RepositoryStatus.Ok;
                return path;
            }
            RepositoryStatus = RepositoryStatus.Cancel;
            return null;
        }

        public RepositoryStatus RepositoryStatus { get; set; }

        #endregion

        #region Private methmods

        private XElement GetXElementResource(string path)
        {
            var assembly = Assembly.GetAssembly(typeof(ProjectRoot));
            using (var stream = assembly.GetManifestResourceStream(path))
            {
                return XElement.Load(stream);
            }
        }

        private bool CheckFiles(string folderPath)
        {
            var chk = File.Exists(folderPath + InfoSubPath) && File.Exists(folderPath + ConfigurationSubPath);
            if (!chk) RepositoryStatus = RepositoryStatus.CantSelectFolder;
            return chk;
        }

        private ProjectRoot LoadProjectIfPathNull()
        {
            var folderPath = GetFolderPath();
            return folderPath != null ? LoadProjectFromPath(folderPath) : null;
        }

        private ProjectRoot LoadProjectFromPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new FileNotFoundException();
            if (!CheckFiles(path)) return null;
            var projectRoot = _serviceLocator.GetInstance<ProjectRoot>();
            try
            {
                projectRoot.FolderPath = path;
                projectRoot.ModuleInformation = _dataService.DeserializeObject<ModuleInformation>(path + InfoSubPath);
                projectRoot.ModuleConfiguration =
                    _dataService.DeserializeObject<ModuleConfiguration>(path + ConfigurationSubPath);
                RepositoryStatus = RepositoryStatus.Ok;
                return projectRoot;
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
                RepositoryStatus = RepositoryStatus.Error;
            }
            return null;
        }

        private bool SaveProjectIfPathNull()
        {
            if (_projectRoot == null)
                throw new FileNotFoundException();
            var folderPath = GetFolderPath();
            if (!SaveProjectFromPath(folderPath)) return false;
            _projectRoot.FolderPath = folderPath;
            return true;
        }

        private bool SaveProjectFromPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new FileNotFoundException();
            Directory.CreateDirectory(path + @"\fomod\");
            try
            {
                _dataService.SerializeObject(_projectRoot.ModuleInformation, path + InfoSubPath);
                _dataService.SerializeObject(_projectRoot.ModuleConfiguration, path + ConfigurationSubPath);
                RepositoryStatus = RepositoryStatus.Ok;
                return true;
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
                RepositoryStatus = RepositoryStatus.Error;
            }
            return false;
        }

        private string GetFolderPath()
        {
            _folderBrowserDialog.Reset();
            var result = _folderBrowserDialog.ShowDialog();
            if (result && _folderBrowserDialog.SelectedPath != null)
                return _folderBrowserDialog.SelectedPath;
            RepositoryStatus = RepositoryStatus.Cancel;
            return null;
        }

        #endregion

        private const string InfoSubPath = @"\fomod\Info.xml";
        private const string ConfigurationSubPath = @"\fomod\ModuleConfig.xml";
        private const string InfoResourcePath = "FomodModel.XmlTemplates.Info.xml";
        private const string ConfigResourcePath = "FomodModel.XmlTemplates.ModuleConfig.xml";

        private ProjectRoot _projectRoot;
        
        public Repository(IServiceLocator serviceLocator, ILogger logger, IFolderBrowserDialog folderBrowserDialog, IDataService dataService)
        {
            _serviceLocator = serviceLocator;
            _logger = logger;
            _dataService = dataService;
            _folderBrowserDialog = folderBrowserDialog;

            _logger.LogCreate(this);
        }

        ~Repository()
        {
            _logger.LogDisposable(this);
        }

    }
}