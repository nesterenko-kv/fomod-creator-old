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
    public class MainEditorViewModel: Prism.Mvvm.BindableBase
    {
        XmlDataProvider _xmlData;

        private const string InfoSubPath = @"\fomod\info.xml";
        private const string ConfigurationSubPath = @"\fomod\ModuleConfig.xml";

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
        public string Header { get; set; }

        IRepository<XmlDataProvider> _repository;

        public MainEditorViewModel(IRepository<XmlDataProvider> repository)
        {
            _repository = repository;
        }


        
    }
}
