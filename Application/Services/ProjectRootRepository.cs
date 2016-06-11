using System;
using System.IO;
using FomodInfrastructure.Interfaces;
using FomodModel.Base;
using Microsoft.Practices.ServiceLocation;

namespace MainApplication.Services
{
    public class ProjectRootRepository : BaseRepository<ProjectRoot>
    {
        private readonly IAppService _appService;

        private readonly IDataService _dataService;

        private readonly ILogger _logger;

        private readonly IServiceLocator _serviceLocator;

        public ProjectRootRepository(IServiceLocator serviceLocator, IDataService dataService, IAppService appService, ILogger logger)
        {
            _serviceLocator = serviceLocator;
            _dataService = dataService;
            _appService = appService;
            _logger = logger;
            _logger.LogCreate(this);
        }

        ~ProjectRootRepository()
        {
            _logger.LogDisposable(this);
        }

        protected override ProjectRoot LoadData(string path)
        {
            var projectRoot = _serviceLocator.GetInstance<ProjectRoot>();
            projectRoot.DataSource = path;
            projectRoot.ModuleInformation = _dataService.LoadData<ModuleInformation>(Path.Combine(path, InfoSubPath));
            projectRoot.ModuleConfiguration = _dataService.LoadData<ModuleConfiguration>(Path.Combine(path, ConfigurationSubPath));
            return projectRoot;
        }

        protected override void SaveData(ProjectRoot data, string path)
        {
            _dataService.SaveData(Data.ModuleInformation, Path.Combine(path, InfoSubPath));
            _dataService.SaveData(Data.ModuleConfiguration, Path.Combine(path, ConfigurationSubPath));
        }

        protected override ProjectRoot CreateData(string path)
        {
            if (Data != null)
            {
                var sourceDir = Path.GetFullPath(Data.DataSource);
                var destinationDir = Path.GetFullPath(path);
                if (!string.Equals(sourceDir, destinationDir, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var dir in Directory.GetDirectories(sourceDir, @"*", SearchOption.AllDirectories))
                        Directory.CreateDirectory(destinationDir + dir.Substring(sourceDir.Length));
                    foreach (var file in Directory.GetFiles(sourceDir, @"*", SearchOption.AllDirectories))
                        File.Copy(file, destinationDir + file.Substring(sourceDir.Length));
                }
            }
            Directory.CreateDirectory(Path.Combine(path, ProjectFolder));
            _appService.GetXElementResource(InfoResourcePath).Save(Path.Combine(path, InfoSubPath));
            _appService.GetXElementResource(ConfigResourcePath).Save(Path.Combine(path, ConfigurationSubPath));
            var projectRoot = _serviceLocator.GetInstance<ProjectRoot>();
            projectRoot.DataSource = path;
            projectRoot.ModuleInformation = _dataService.LoadData<ModuleInformation>(Path.Combine(path, InfoSubPath));
            projectRoot.ModuleConfiguration = _dataService.LoadData<ModuleConfiguration>(Path.Combine(path, ConfigurationSubPath));
            return projectRoot;
        }

        protected override void AfterCreate(RepositoryResult<ProjectRoot> result)
        {
            if (!result.Success)
                _logger.Log(result.Exception);
        }

        protected override void AfterLoad(RepositoryResult<ProjectRoot> result)
        {
            if (!result.Success)
                _logger.Log(result.Exception);
        }

        protected override void AfterSave(RepositoryResult result)
        {
            if (!result.Success)
                _logger.Log(result.Exception);
        }

        #region Constants

        private const string ProjectFolder = @"fomod";

        private const string InfoSubPath = ProjectFolder + @"\Info.xml";

        private const string ConfigurationSubPath = ProjectFolder + @"\ModuleConfig.xml";

        private const string InfoResourcePath = @"FomodModel.XmlTemplates.Info.xml";

        private const string ConfigResourcePath = @"FomodModel.XmlTemplates.ModuleConfig.xml";

        #endregion
    }
}