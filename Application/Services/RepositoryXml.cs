using FomodInfrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MainApplication.Services
{
    public class RepositoryXml : IRepository<XElement>
    {
        private const string InfoSubPath = @"\fomod\info.xml";
        private const string ConfigurationSubPath = @"\fomod\ModuleConfig.xml";


        public XElement GetData()
        {
            throw new NotImplementedException();
        }

        public XElement LoadData(string path = null)
        {
            return path != null ? XElement.Load(path + ConfigurationSubPath) : null;
        }

        public bool SaveData(string path = null)
        {
            throw new NotImplementedException();
        }
    }
}
