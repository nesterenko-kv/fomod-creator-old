using FomodInfrastructure.Interface;
using FomodModel.Base;
using Microsoft.Practices.ServiceLocation;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorNew.ViewModel
{
    public class MainEditorViewModel: BaseViewModel<ProjectRoot>
    {
        public string Header { get; set; } = "Новый редактор";


        #region Services

        IRegionManager _regionManager;
        IRepository<ProjectRoot> _repository;

        private readonly ILogger _logger;

        #endregion


        public void ConfigurateViewModel(IRegionManager regionManager, IRepository<ProjectRoot> repository)
        {
            _regionManager = regionManager;
            _repository = repository;

            Data = _repository.GetData();
        }

        public MainEditorViewModel(ILogger logger)
        {
            _logger = logger;
            _logger.LogCreate(this);
        }

        ~MainEditorViewModel()
        {
            _logger.LogDisposable(this);
        }
    }
}
