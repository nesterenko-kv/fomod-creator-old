using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base;

namespace Module.Welcome.ViewModel
{
    public class WelcomeViewModel
    {
        #region Fields
       
        ICommand _closeApplication;
        ICommand _openProject;
        ICommand _createProject;

        #endregion

        private readonly IAppService AppService;
        private readonly IRepository<ProjectRoot> Repository;

        public WelcomeViewModel(IAppService AppService, IRepository<ProjectRoot> Repository)
        {
            this.AppService = AppService;
            this.Repository = Repository;
        }




        #region Commands

        public ICommand CloseApplication
        {
            get
            {
                if (_closeApplication == null)
                    _closeApplication = new RelayCommand((p) =>
                    {
                        this.AppService.CloseApp();
                    });
                return _closeApplication;
            }
        }
        public ICommand OpenProject
        {
            get
            {
                if (_openProject == null)
                    _openProject = new RelayCommand((p) =>
                    {
                        var Data = this.Repository.LoadData();
                    });
                return _openProject;
            }
        } 

        public ICommand CreateProject
        {
            get
            {
                if (_createProject == null)
                    _createProject = new RelayCommand((p) =>
                    {
                        
                    });
                return _createProject;
            }
        } 


        #endregion
    }
}
