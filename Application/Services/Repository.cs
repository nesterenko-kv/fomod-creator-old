using System;
using System.IO;
using FomodInfrastructure.Interface;
using FomodModel.Base;
using Microsoft.Practices.ServiceLocation;
using Prism.Logging;

namespace MainApplication.Services
{
    public class Repository : IRepository<ProjectRoot>
    {
        private const string InfoSubPath = @"\fomod\info.xml";
        private const string ConfigurationSubPath = @"\fomod\ModuleConfig.xml";
        private readonly IDataService _dataService;
        private readonly IFolderBrowserDialog _folderBrowserDialog;
        private readonly ILoggerFacade _loggerFacade;

        private readonly IServiceLocator _serviceLocator;
        private ProjectRoot _projectRoot;

        public Repository(IServiceLocator serviceLocator, ILoggerFacade loggerFacade,
            IFolderBrowserDialog folderBrowserDialog, IDataService dataService)

        {
            _serviceLocator = serviceLocator;
            _loggerFacade = loggerFacade;
            _dataService = dataService;
            _folderBrowserDialog = folderBrowserDialog;
        }

        #region IRepository

        public ProjectRoot LoadData(string path = null)
            => _projectRoot = path != null ? LoadProjectFromPath(path) : LoadProjectIfPathNull();

        public bool SaveData(string path = null) => path != null ? SaveProjectFromPath(path) : SaveProjectIfPathNull();

        public ProjectRoot GetData() => _projectRoot;

        public string CurrentPath
        {
            get { return _projectRoot.FolderPath; }
            set { _projectRoot.FolderPath = value; }
        }

        public RepositoryStatus RepositoryStatus { get; set; }

        #endregion

        #region Private methmods

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
                _loggerFacade.Log(e.Message, Category.Exception, Priority.Medium);
                RepositoryStatus = RepositoryStatus.Error;
            }
            return null;
        }

        private bool SaveProjectIfPathNull()
        {
            if (_projectRoot == null)
                throw new FileNotFoundException();
            var folderPath = GetFolderPath();
            return folderPath != null && SaveProjectFromPath(folderPath);
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
                _loggerFacade.Log(e.Message, Category.Exception, Priority.Medium);
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
    }
}