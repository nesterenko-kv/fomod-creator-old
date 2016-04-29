using FomodInfrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public XmlDataProvider GetData()
        {
            return _xmlData;
        }

        public XmlDataProvider LoadData(string path = null)
        {
            _xmlData = new XmlDataProvider();

            var projectXml = new XmlDocument();

            var info = XElement.Load(path + InfoSubPath).ToString();
            var config = XElement.Load(path + ConfigurationSubPath).ToString();
            var project = "<Project>" + info + config + "</Project>";

            projectXml.LoadXml(project);

            _xmlData.Document = projectXml;

            return _xmlData;
        }

        public bool SaveData(string path = null)
        {
            //_xmlData.Document.DocumentElement.ChildNodes[0].OwnerDocument.Save("");
           
            throw new NotImplementedException();
        }
    }
}
