using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodInfrastructure.Interface;
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
    public class MainEditorViewModel: Prism.Mvvm.BindableBase /*, INavigationAware*/
    {

        XmlDataProvider _xmlData;


        public XmlDataProvider XmlData
        {
            get
            {
                if (_xmlData == null)
                    _xmlData = _repository.GetData();
                return _xmlData;
            }
        }

        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public string Header { get; set; } = "Редактор";

        IRepository<XmlDataProvider> _repository;

        public MainEditorViewModel(IRepository<XmlDataProvider> repository)
        {
            _repository = repository;
        }

        //public void OnNavigatedTo(NavigationContext navigationContext)
        //{
        //}

        //public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        //public void OnNavigatedFrom(NavigationContext navigationContext)
        //{
        //}
    }
}
