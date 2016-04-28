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
        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public XElement XmlDocumentA { get; set; }

        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public XmlDataProvider XmlDataProvider { get; set; }

        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public string Header { get; set; }


        public MainEditorViewModel()
        {

        }


        #region INavigationAware

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var p = navigationContext.Parameters["xml"] as XElement;
            if (p == null) throw new NullReferenceException("В параметрах отсутствует нужный тип данных");

            //XmlDocumentA = p;
            //var ddd = XDocument.Parse(p.ToString());
            //XmlDataProvider = new XmlDataProvider();
            //XmlDataProvider.Document = (XmlDocument)ddd;
            //XmlDataProvider.XPath = "node";

            Header = "ProjectEdit";
        }

        #endregion
    }
}
