using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FomodInfrastructure.MvvmLibrary.Commands;

namespace Module.Welcome.ViewModel
{
    public class WelcomeViewModel
    {
        #region Fields
       
        ICommand _closeApplication;
        ICommand _openProject;

        #endregion

        private readonly IAppService AppService;

        public WelcomeViewModel(IAppService AppService)
        {
            this.AppService = AppService;
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

                    });
                return _openProject;
            }
        } 

        #endregion
    }
}
