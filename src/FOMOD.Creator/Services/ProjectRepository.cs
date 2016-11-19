namespace FOMOD.Creator.Services
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Xml.Linq;
    using FOMOD.Creator.Domain;
    using FOMOD.Creator.Domain.Models;
    using FOMOD.Creator.Interfaces;

    public class ProjectManager : IProjectManager
    {
        private static readonly string ConfigResourcePath = "FOMOD.Creator.Domain.Templates.ModuleConfig.xml";
        private static readonly string ProjectFolder = "fomod";
        private static readonly string ConfigurationSubPath = Path.Combine(ProjectFolder, "ModuleConfig.xml");
        private static readonly string InfoResourcePath = "FOMOD.Creator.Domain.Templates.Info.xml";
        private static readonly string InfoSubPath = Path.Combine(ProjectFolder, "Info.xml");


        private readonly IDataService _dataService;

        public ProjectManager(IDataService dataService)
        {
            _dataService = dataService;
        }

        public Project Data { get; private set; }

        public Result<Project> Create(string path)
        {
            Result<Project> result;
            try
            {
                Data = CreateData(path);
                result = new Result<Project>(Data);
            }
            catch (Exception e)
            {
                result = new Result<Project>(e);
            }
            return result;
        }

        public Result<Project> Load(string path)
        {
            Result<Project> result;
            try
            {
                Data = LoadData(path);
                result = new Result<Project>(Data);
            }
            catch (Exception e)
            {
                result = new Result<Project>(e);
            }
            return result;
        }


        public void Save(string path)
        {
            _dataService.SaveData(Data.Info, Path.Combine(path, InfoSubPath));
            _dataService.SaveData(Data.Config, Path.Combine(path, ConfigurationSubPath));
        }


        private static XElement GetXElementResource(string path)
        {
            var assembly = Assembly.GetAssembly(typeof(Project));
            using (var stream = assembly.GetManifestResourceStream(path))
            {
                return XElement.Load(stream);
            }
        }

        private Project CreateData(string path)
        {
            if (Data != null)
            {
                var sourceDir = Path.GetFullPath(Data.Source);
                var destinationDir = Path.GetFullPath(path);
                if (!string.Equals(sourceDir, destinationDir, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var dir in Directory.GetDirectories(sourceDir, "*", SearchOption.AllDirectories))
                        Directory.CreateDirectory(destinationDir + dir.Substring(sourceDir.Length));
                    foreach (var file in Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories))
                        File.Copy(file, destinationDir + file.Substring(sourceDir.Length));
                }
            }
            Directory.CreateDirectory(Path.Combine(path, ProjectFolder));
            GetXElementResource(InfoResourcePath).Save(Path.Combine(path, InfoSubPath));
            GetXElementResource(ConfigResourcePath).Save(Path.Combine(path, ConfigurationSubPath));
            var project = new Project(path)
            {
                Info = _dataService.LoadData<ModuleInformation>(Path.Combine(path, InfoSubPath)),
                Config = _dataService.LoadData<ModuleConfiguration>(Path.Combine(path, ConfigurationSubPath))
            };
            return project;
        }

        private Project LoadData(string path)
        {
            var project = new Project(path)
            {
                Info = _dataService.LoadData<ModuleInformation>(Path.Combine(path, InfoSubPath)),
                Config = _dataService.LoadData<ModuleConfiguration>(Path.Combine(path, ConfigurationSubPath))
            };
            return project;
        }
    }
}
