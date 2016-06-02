using FomodInfrastructure.Interface;
using FomodModel.Base;
using Prism.Regions;

namespace EditorNew.ViewModel
{
    public class MainEditorViewModel : BaseViewModel<ProjectRoot>
    {
        public MainEditorViewModel(ILogger logger)
        {
            _logger = logger;
            _logger.LogCreate(this);
        }

        public string Header { get; set; } = "Новый редактор";

        public void ConfigurateViewModel(IRegionManager regionManager, IRepository<ProjectRoot> repository)
        {
            _regionManager = regionManager;
            _repository = repository;

            Data = _repository.GetData();
        }

        ~MainEditorViewModel()
        {
            _logger.LogDisposable(this);
        }

        #region Services

        private IRegionManager _regionManager;

        private IRepository<ProjectRoot> _repository;

        private readonly ILogger _logger;

        #endregion
    }
}