using System;
using System.IO;
using System.Xml.Serialization;
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

        public Repository(IServiceLocator serviceLocator, ILoggerFacade loggerFacade, IFolderBrowserDialog folderBrowserDialog)
        {
            _serviceLocator = serviceLocator;
            _loggerFacade = loggerFacade;
            _folderBrowserDialog = folderBrowserDialog;
        }

        #region IRepository
        public ProjectRoot LoadData(string path = null) => _projectRoot = path != null ? LoadProjectFromPath(path) : LoadProjectIfPathNull();

        public bool SaveData(string path = null) => path != null ? SaveProjectFromPath(path) : SaveProjectIfPathNull();

        public ProjectRoot GetData() => _projectRoot;
        #endregion

        #region Private methmods
        private void SerializeObject<T>(T data, string path)
        {
            if (data == null) return;
            using (var fs = File.Create(path))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(fs, data);
            }
        }

        private T DeserializeObject<T>(string path)
        {
            using (var fs = File.OpenRead(path))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(fs);
            }
        }

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
            if (!CheckFiles(path)) return null; //зделал возврат нулевой что бы можно было делать обработку в viewmodel
            var projectRoot = _serviceLocator.GetInstance<ProjectRoot>();
            try
            {
                projectRoot.ModuleInformation = DeserializeObject<ModuleInformation>(path + InfoSubPath);
                projectRoot.ModuleConfiguration = DeserializeObject<ModuleConfiguration>(path + ConfigurationSubPath);
                return projectRoot;
            }
            catch (Exception)
            {
                //TODO: если ошибка то позже сделаем сервис оповещения об ошибках
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
                SerializeObject(_projectRoot.ModuleInformation, path + InfoSubPath);
                SerializeObject(_projectRoot.ModuleConfiguration, path + ConfigurationSubPath);
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