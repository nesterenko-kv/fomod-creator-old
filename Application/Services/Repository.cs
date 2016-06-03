using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using FomodInfrastructure;
using FomodInfrastructure.Interface;
using FomodModel.Base;
using Microsoft.Practices.ServiceLocation;

namespace MainApplication.Services
{
    public class Repository : IRepository<ProjectRoot>
    {
        private const string ProjectFolder = @"fomod";

        private const string InfoSubPath = ProjectFolder + @"\Info.xml";

        private const string ConfigurationSubPath = ProjectFolder + @"\ModuleConfig.xml";

        private const string InfoResourcePath = @"FomodModel.XmlTemplates.Info.xml";

        private const string ConfigResourcePath = @"FomodModel.XmlTemplates.ModuleConfig.xml";

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

        #region Services

        private readonly IDataService _dataService;

        private readonly IFolderBrowserDialog _folderBrowserDialog;

        private readonly ILogger _logger;

        private readonly IServiceLocator _serviceLocator;

        #endregion

        #region IRepository

        public ProjectRoot LoadData(string path = null)
        {
            return _projectRoot = path != null ? LoadDataFromPath(path) : LoadDataIfPathNull();
        }

        public bool SaveData(string path = null)
        {
            return path != null ? SaveDataToPath(path) : SaveDataIfPathNull();
        }

        public ProjectRoot GetData()
        {
            return _projectRoot;
        }

        public string CurrentPath
        {
            get { return _projectRoot.FolderPath; }
            set { _projectRoot.FolderPath = value; }
        }

        public string CreateData()
        {
            var path = GetFolderPath();
            if (CheckFiles(path))
            {
                RepositoryStatus = RepositoryStatus.FolderIsAlreadyUsed;
                return null;
            }
            try
            {
                Directory.CreateDirectory(Path.Combine(path, ProjectFolder));
                GetXElementResource(InfoResourcePath).Save(Path.Combine(path, InfoSubPath));
                GetXElementResource(ConfigResourcePath).Save(Path.Combine(path, ConfigurationSubPath));
                RepositoryStatus = RepositoryStatus.Ok;
                return path;
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
                RepositoryStatus = RepositoryStatus.Error;
            }
            return null;
        }

        public RepositoryStatus RepositoryStatus { get; set; }

        #endregion

        #region Methods

        private static XElement GetXElementResource(string path)
        {
            var assembly = Assembly.GetAssembly(typeof(ProjectRoot));
            using (var stream = assembly.GetManifestResourceStream(path))
                return XElement.Load(stream);
        }
        
        private ProjectRoot LoadDataIfPathNull()
        {
            var folderPath = GetFolderPath();
            return folderPath != null ? LoadDataFromPath(folderPath) : null;
        }

        private ProjectRoot LoadDataFromPath(string path)
        {
            if (!CheckFiles(path))
                return null;
            var projectRoot = _serviceLocator.GetInstance<ProjectRoot>();
            try
            {
                projectRoot.FolderPath = path;
                projectRoot.ModuleInformation = _dataService.DeserializeObject<ModuleInformation>(Path.Combine(path, InfoSubPath));
                projectRoot.ModuleConfiguration = _dataService.DeserializeObject<ModuleConfiguration>(Path.Combine(path, ConfigurationSubPath));
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

        private bool SaveDataIfPathNull()
        {
            if (_projectRoot == null)
                throw new ArgumentNullException(nameof(_projectRoot));
            var folderPath = GetFolderPath();
            if (RepositoryStatus == RepositoryStatus.Cancel)
                return false;
            if (!SaveDataToPath(folderPath))
                return false;
            _projectRoot.FolderPath = folderPath;
            return true;
        }

        private bool SaveDataToPath(string path)
        {
            try
            {
                Directory.CreateDirectory(Path.Combine(path, ProjectFolder));
                _dataService.SerializeObject(_projectRoot.ModuleInformation, Path.Combine(path, InfoSubPath));
                _dataService.SerializeObject(_projectRoot.ModuleConfiguration, Path.Combine(path, ConfigurationSubPath));
                RepositoryStatus = RepositoryStatus.Ok;
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
                RepositoryStatus = RepositoryStatus.Error;
            }
            return RepositoryStatus == RepositoryStatus.Ok;
        }

        private bool CheckFiles(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
                return false;
            var chk = File.Exists(Path.Combine(folderPath, InfoSubPath)) && File.Exists(Path.Combine(folderPath, ConfigurationSubPath));
            if (!chk)
                RepositoryStatus = RepositoryStatus.CantUseFolder;
            return chk;
        }

        private string GetFolderPath()
        {
            _folderBrowserDialog.Reset();
            _folderBrowserDialog.CheckFolderExists = true;
            var result = _folderBrowserDialog.ShowDialog();
            if (result)
                return _folderBrowserDialog.SelectedPath;
            RepositoryStatus = RepositoryStatus.Cancel;
            return null;
        }

        #endregion
    }
}