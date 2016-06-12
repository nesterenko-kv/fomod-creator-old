using System;
using System.IO;
using FomodInfrastructure.Interfaces;
using FomodModel.Base;

namespace MainApplication.Services
{
    public class ProjectRepository : BaseRepository<Project>
    {
        private readonly IAppService _appService;

        private readonly IDataService _dataService;

        private readonly ILogger _logger;
        
        public ProjectRepository(IDataService dataService, IAppService appService, ILogger logger)
        {
            _dataService = dataService;
            _appService = appService;
            _logger = logger;
            _logger.LogCreate(this);
        }

        ~ProjectRepository()
        {
            _logger.LogDisposable(this);
        }

        protected override Project LoadData(string path)
        {
            var project = Project.Create(path);
            project.ModuleInformation = _dataService.LoadData<ModuleInformation>(Path.Combine(path, InfoSubPath));
            project.ModuleConfiguration = _dataService.LoadData<ModuleConfiguration>(Path.Combine(path, ConfigurationSubPath));
            return project;
        }

        protected override void SaveData(Project data, string path)
        {
            _dataService.SaveData(Data.ModuleInformation, Path.Combine(path, InfoSubPath));
            _dataService.SaveData(Data.ModuleConfiguration, Path.Combine(path, ConfigurationSubPath));
        }

        protected override Project CreateData(string path)
        {
            if (Data != null)
            {
                var sourceDir = Path.GetFullPath(Data.Source);
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
            var project = Project.Create(path);
            project.ModuleInformation = _dataService.LoadData<ModuleInformation>(Path.Combine(path, InfoSubPath));
            project.ModuleConfiguration = _dataService.LoadData<ModuleConfiguration>(Path.Combine(path, ConfigurationSubPath));
            return project;
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