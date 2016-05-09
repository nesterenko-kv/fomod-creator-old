using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodInfrastructure.Interface;
using FomodModel.Base;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Linq;

namespace Module.Editor.ViewModel
{
    public class EditorViewModel: BindableBase, INavigationAware
    {
        public string Header { get; } = "Редактор";

        #region Properties

        private object _node;

        //[Aspect(typeof(AcpectDebugLoger))]
        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public object CurentSelectedItem
        {
            get { return _node; }
            set
            { 
                _node = value;
                if (value == null) return;

                var name = value.GetType().Name;

                var param = new NavigationParameters
                {
                    { name, value }
                };
                _regionManager.Regions["NodeRegion"].RequestNavigate(name + "View", param);
            }
        }

        private ObservableCollection<ProjectRoot> _data = new ObservableCollection<ProjectRoot>();

        [Aspect(typeof(AcpectDebugLoger))]
        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public ObservableCollection<ProjectRoot> Data
        {
            get
            {
                var data = _repository.GetData();
                if (_data.FirstOrDefault(i=>i.FolderPath== data .FolderPath) == null)
                    _data.Add(_repository.GetData());
                return _data; //return _repository.GetData();
            }
            set
            {
                _data = value;
            }
        }

        #endregion

        #region Services

        private readonly IRepository<ProjectRoot> _repository;
        private readonly IRegionManager _regionManager;

        #endregion

        #region INavigationAware

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        #endregion

        public EditorViewModel(IRepository<ProjectRoot> repository, IRegionManager regionManager)
        {
            _repository = repository;
            _regionManager = regionManager;
        }

    }
}
