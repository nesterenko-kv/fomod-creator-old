using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodModel.Base;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml;
using System.Xml.Linq;

namespace Module.Editor.ViewModel
{
    public class MainEditorViewModel: INavigationAware
    {
        private const string InfoSubPath = @"\fomod\info.xml";
        private const string ConfigurationSubPath = @"\fomod\ModuleConfig.xml";

        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public XElement XmlDocumentA { get; set; }

        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public XmlDataProvider XmlData { get; set; }

        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public string Header { get; set; }


        public MainEditorViewModel()
        {

        }


        #region INavigationAware

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //XmlDocument
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var folderPath = navigationContext.Parameters["folderPath"]?.ToString();
            if (string.IsNullOrWhiteSpace(folderPath)) throw new NullReferenceException("В параметрах отсутствует нужный тип данных");

            //XmlDocumentA = p;
            //var ddd = XDocument.Parse(p.ToString());
            //XmlDataProvider = new XmlDataProvider();
            //XmlDataProvider.Document = (XmlDocument)ddd;
            //XmlDataProvider.XPath = "node";
            XmlData = new XmlDataProvider();
            //XmlData.Source = new Uri(folderPath + ConfigurationSubPath);
            XmlDocument ProjectXml = new XmlDocument();

            var info = XElement.Load(folderPath + InfoSubPath).ToString();
            var config = XElement.Load(folderPath + ConfigurationSubPath).ToString();
            var project = "<Project>" + info + config + "</Project>";

            ProjectXml.LoadXml(project);

            //TODO при сохранении документа надо учитывать что у нас дублируется имя мода в конфиге и в инфо

            //ProjectXml.Load(folderPath + ConfigurationSubPath);

            XmlData.Document = ProjectXml;
            Header = "ProjectEdit";
        }

        #endregion
    }
}
