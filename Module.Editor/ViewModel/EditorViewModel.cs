using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodInfrastructure.Interface;
using System.Windows.Data;
using Prism.Mvvm;
using System.Xml;
using Prism.Regions;

namespace Module.Editor.ViewModel
{
    public class EditorViewModel: BindableBase
    {
        private XmlElement _node;

        #region Properties

        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public string Header { get; set; } = "Редактор";

        [Aspect(typeof(AcpectDebugLoger))]
        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public XmlElement CurentSelectedXmlNode
        {
            get { return _node; }
            set
            { 
                _node = value;
                if (value == null) return;

                var param = new NavigationParameters
                {
                    { value.Name, value }
                };
                _regionManager.Regions["NodeRegion"].RequestNavigate(value.Name + "View", param);
            }
        }
        #endregion

        #region Services

        private readonly IRepository<XmlDataProvider> _repository;
        private readonly IRegionManager _regionManager;

        #endregion

        public EditorViewModel(IRepository<XmlDataProvider> repository, IRegionManager regionManager)
        {
            _repository = repository;
            _regionManager = regionManager;
        }
       
        private XmlDataProvider _xmlData;
        public XmlDataProvider XmlData
        {
            get
            {  
                if (_xmlData == null)
                    _xmlData = _repository.GetData();
                return _xmlData;
            }
        }
    }
}
