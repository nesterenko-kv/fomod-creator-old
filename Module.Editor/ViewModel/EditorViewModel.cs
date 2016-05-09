using System.Collections.ObjectModel;
using System.Linq;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodInfrastructure.Interface;
using FomodModel.Base;
using Prism.Mvvm;
using Prism.Regions;

namespace Module.Editor.ViewModel
{
    [Aspect(typeof (AspectINotifyPropertyChanged))]
    public class EditorViewModel : BindableBase
    {
        public EditorViewModel(IRepository<ProjectRoot> repository)
        {
            _repository = repository;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public string Header { get; private set; } = "Редактор";

        public void ConfigurateViewModel(IRegionManager regionManager, ProjectRoot projectRoot, string header = null)
        {
            if (header != null)
                Header = header;
            else
            {
                var name = projectRoot.ModuleInformation.Name;
                Header = string.IsNullOrWhiteSpace(name) ? Header : name;
            }

            var pRoot = Data.FirstOrDefault(i => i.FolderPath == projectRoot.FolderPath);
            if (pRoot == null)
                Data.Add(projectRoot);


            _regionManager = regionManager;
        }

        #region Properties

        private object _selectedNode;

        public object SelectedNode
        {
            get { return _selectedNode; }
            set
            {
                _selectedNode = value;
                if (value == null) return;

                var name = value.GetType().Name;

                var param = new NavigationParameters
                {
                    {name, value}
                };
                _regionManager.Regions["NodeRegion"].RequestNavigate(name + "View", param);
            }
        }

        public ObservableCollection<ProjectRoot> Data { get; set; } = new ObservableCollection<ProjectRoot>();

        #endregion

        #region Services

        private readonly IRepository<ProjectRoot> _repository;
        private IRegionManager _regionManager;

        #endregion
    }
}