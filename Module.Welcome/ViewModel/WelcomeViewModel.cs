using System.Windows.Input;
using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base;

namespace Module.Welcome.ViewModel
{
    public class WelcomeViewModel
    {
        #region Fields

        private ICommand _closeApplication;
        private ICommand _openProject;
        private ICommand _createProject;

        #endregion

        private readonly IAppService _appService;
        private readonly IRepository<ProjectRoot> _repository;

        public WelcomeViewModel(IAppService appService, IRepository<ProjectRoot> repository)
        {
            _appService = appService;
            _repository = repository;
        }

        #region Commands

        public ICommand CloseApplication
        {
            get
            {
                if (_closeApplication == null)
                    _closeApplication = new RelayCommand(p => _appService.CloseApp());
                return _closeApplication;
            }
        }
        public ICommand OpenProject
        {
            get
            {
                if (_openProject == null)
                    _openProject = new RelayCommand(p =>
                    {
                        var data = _repository.LoadData();
                    });
                return _openProject;
            }
        } 

        public ICommand CreateProject
        {
            get
            {
                if (_createProject == null)
                    _createProject = new RelayCommand(p =>
                    {
                        
                    });
                return _createProject;
            }
        }
        #endregion
    }
}
