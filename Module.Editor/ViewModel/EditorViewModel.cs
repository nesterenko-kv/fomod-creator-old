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
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    public class EditorViewModel : BindableBase
    {
        public string Header { get; private set; } = "Редактор";

        #region Properties

        private object _node;

        //[Aspect(typeof(AcpectDebugLoger))]
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

        
        public ObservableCollection<ProjectRoot> Data { get; set; } = new ObservableCollection<ProjectRoot>();
        //{
        //    get
        //    {
        //        var data = _repository.GetData();
        //        if (_data.FirstOrDefault(i => i.FolderPath == data.FolderPath) == null)
        //            _data.Add(_repository.GetData());
        //        return _data; //return _repository.GetData();
        //    }
        //    set
        //    {
        //        _data = value;
        //    }
        //}

        #endregion

        #region Services

        private readonly IRepository<ProjectRoot> _repository;
        private IRegionManager _regionManager;

        #endregion

        public EditorViewModel(IRepository<ProjectRoot> repository)
        {
            _repository = repository;
        }

        public void ConfigurateViewModel(IRegionManager regionManager, ProjectRoot projectRoot, string header = null)
        {
            if (header!=null)
                this.Header = header;
            else
            {
                var name = projectRoot.ModuleInformation.Name;
                this.Header = string.IsNullOrWhiteSpace(name) ? this.Header : name;
            }

            var pRoot = Data.FirstOrDefault(i => i.FolderPath == projectRoot.FolderPath);
            if (pRoot==null)
                Data.Add(projectRoot);

            
            _regionManager = regionManager;
        }
    }
}
