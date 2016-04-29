using FomodInfrastructure.Interface;
using System;
using System.IO;
using System.Windows.Data;
using System.Xml;
using System.Xml.Linq;

namespace MainApplication.Services
{
    public class RepositoryXml : IRepository<XmlDataProvider>
    {
        private const string InfoSubPath = @"\fomod\info.xml";
        private const string ConfigurationSubPath = @"\fomod\ModuleConfig.xml";
        private XmlDataProvider _xmlData;

        private readonly IFolderBrowserDialog _folderBrowserDialog;

        public RepositoryXml(IFolderBrowserDialog folderBrowserDialog)
        {
            _folderBrowserDialog = folderBrowserDialog;
        }

        public XmlDataProvider GetData() => _xmlData;

        public XmlDataProvider LoadData(string path = null) => _xmlData = path != null ? LoadProjectFromPath(path) : LoadProjectIfPathNull();

        private XmlDataProvider LoadProjectIfPathNull()
        {
            var folderPath = GetFolderPath();
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
        private bool CheckFiles(string folderPath) => File.Exists(folderPath + InfoSubPath) && File.Exists(folderPath + ConfigurationSubPath);
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