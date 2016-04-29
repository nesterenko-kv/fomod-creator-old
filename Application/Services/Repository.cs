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
        private ProjectRoot _projectRoot;

        private readonly IServiceLocator _serviceLocator;
        private readonly ILoggerFacade _loggerFacade;
        private readonly IFolderBrowserDialog _folderBrowserDialog;
        private readonly IDataService _dataService;

        public Repository(IServiceLocator serviceLocator, ILoggerFacade loggerFacade, IFolderBrowserDialog folderBrowserDialog, IDataService dataService)
        
        {
            _serviceLocator = serviceLocator;
            _loggerFacade = loggerFacade;
            _dataService = dataService;
            _folderBrowserDialog = folderBrowserDialog;
        }

        #region IRepository
        public ProjectRoot LoadData(string path = null) => _projectRoot = path != null ? LoadProjectFromPath(path) : LoadProjectIfPathNull();

        public bool SaveData(string path = null) => path != null ? SaveProjectFromPath(path) : SaveProjectIfPathNull();

        public ProjectRoot GetData() => _projectRoot;
        #endregion

        #region Private methmods

        private bool CheckFiles(string folderPath) => File.Exists(folderPath + InfoSubPath) && File.Exists(folderPath + ConfigurationSubPath);

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
                projectRoot.ModuleConfiguration = _dataService.DeserializeObject<ModuleConfiguration>(path + ConfigurationSubPath);
                return projectRoot;
            }
            catch (Exception)
            {
                //TODO: сделать сервис оповещения об ошибках
            }
            throw new FileNotFoundException();
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
                return true;
            }
            catch (Exception)
            {
                return false; //TODO: обработать ошибки
            }
        }

        private string GetFolderPath()
        {
            _folderBrowserDialog.Reset();
            var result = _folderBrowserDialog.ShowDialog();
            if (result && _folderBrowserDialog.SelectedPath != null)
                return _folderBrowserDialog.SelectedPath;
            return null;
        }
        #endregion
    }
}