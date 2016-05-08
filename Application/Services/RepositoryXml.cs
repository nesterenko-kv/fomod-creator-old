using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodInfrastructure.Interface;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Data;
using System.Xml;
using System.Xml.Linq;

namespace MainApplication.Services
{
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    public class RepositoryXml : IRepository<XmlDataProvider>
    {
        private const string InfoSubPath = @"\fomod\info.xml";
        private const string ConfigurationSubPath = @"\fomod\ModuleConfig.xml";

        #region Services 

        private readonly IFolderBrowserDialog _folderBrowserDialog;
        
        #endregion

        #region IRepository

        public string CurrentPath { get; private set; }
        
        public XmlDataProvider GetData() => _xmlData;

        public XmlDataProvider LoadData(string path = null) => _xmlData = path != null ? LoadProjectFromPath(path) : LoadProjectIfPathNull();

        public string CreateData()
        {
            _folderBrowserDialog.ShowDialog();
            var path = _folderBrowserDialog.SelectedPath;
            _folderBrowserDialog.Reset();
            if (!string.IsNullOrWhiteSpace(path))
            {
                if (CheckFiles(path)) return "error"; //если папка содержит уже проект то его нельзя перезаписывать //TODO решить вопрос с оповещением

                Directory.CreateDirectory(path + @"\fomod");
                Directory.CreateDirectory(path + @"\Data");

                var info = "MainApplication.Resources.xml.infoDefault.xml";
                var config = "MainApplication.Resources.xml.ModuleConfigDefault.xml";

                getXElementResource(info).Save(path + InfoSubPath);
                getXElementResource(config).Save(path + ConfigurationSubPath);
                
                return path;
            }
            return null;
        }

        private XElement getXElementResource(string path)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(path))
            {
                return XElement.Load(stream);
            }
        }

        #endregion

        public RepositoryXml(IFolderBrowserDialog folderBrowserDialog)
        {
            _folderBrowserDialog = folderBrowserDialog;
        }

        private XmlDataProvider _xmlData;
        private XmlDataProvider LoadProjectIfPathNull()
        {
            var folderPath = GetFolderPath();
            CurrentPath = folderPath;
            return folderPath != null ? LoadProjectFromPath(folderPath) : null;
        }
        private XmlDataProvider LoadProjectFromPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new FileNotFoundException();
            if (!CheckFiles(path)) return null;
            try
            {
                _xmlData = new XmlDataProvider();
                var info = XElement.Load(path + InfoSubPath).ToString();
                var config = XElement.Load(path + ConfigurationSubPath).ToString();
                var project = "<Project>" + info + config + "</Project>";
                var projectXml = new XmlDocument();
                projectXml.LoadXml(project);
                _xmlData.Document = projectXml;
                CurrentPath = path;
                return _xmlData;
            }
            catch (Exception)
            {
                //TODO: сделать сервис оповещения об ошибках
            }
            throw new FileNotFoundException();
        }

        public bool SaveData(string path = null)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new FileNotFoundException();
            Directory.CreateDirectory(path + @"\fomod\");
            try
            {
                _xmlData.Document.DocumentElement?.ChildNodes[0].OwnerDocument?.Save(path + InfoSubPath);
                _xmlData.Document.DocumentElement?.ChildNodes[1].OwnerDocument?.Save(path + ConfigurationSubPath);
                return true;
            }
            catch (Exception)
            {
                return false; //TODO: обработать ошибки
            }
        }
        private bool CheckFiles(string folderPath)
        {
            return File.Exists(folderPath + InfoSubPath) && File.Exists(folderPath + ConfigurationSubPath);
        }

        private string GetFolderPath()
        {
            _folderBrowserDialog.Reset();
            var result = _folderBrowserDialog.ShowDialog();
            if (result && _folderBrowserDialog.SelectedPath != null)
                return _folderBrowserDialog.SelectedPath;
            return null;
        }

    }
}