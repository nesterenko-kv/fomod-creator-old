using System.Windows.Data;
using System.Xml;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodInfrastructure.Interface;
using Prism.Mvvm;
using Prism.Regions;

namespace Module.Editor.ViewModel
{
    public class EditorViewModel: BindableBase//, INavigationAware
    {
        public string Header { get;} = "Редактор";
        public string ProjectPath { get; set; } 

        #region Properties

        private XmlElement _node;

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
                //_regionManager.Regions["NodeRegion"].RequestNavigate(value.Name + "View", param);
                RegionManager.Regions["NodeRegion"].RequestNavigate(value.Name + "View", param);
            }
        }

        private XmlDataProvider _xmlData;
        public XmlDataProvider XmlData
        {
            get
            {
                if (_xmlData == null)
                    _xmlData = _repository.GetData();
                return _xmlData; //return _repository.GetData();
            }
            set
            {
                _xmlData = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Services

        private readonly IRepository<XmlDataProvider> _repository;
        private readonly IRegionManager _regionManager;
        public IRegionManager RegionManager { get; set; }

        #endregion

        #region INavigationAware

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        #endregion

        public EditorViewModel(IRepository<XmlDataProvider> repository, IRegionManager regionManager)
        {
            _repository = repository;
            _regionManager = regionManager;

            ProjectPath = _repository.CurrentPath;
        }

    }
}
