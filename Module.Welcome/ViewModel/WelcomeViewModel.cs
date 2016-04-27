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
using Prism.Regions;
using FomodInfrastructure;

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
        private readonly IRegionManager RegionManager;

        public WelcomeViewModel(IAppService AppService, IRepository<ProjectRoot> Repository, IRegionManager RegionManager)
        {
            this.AppService = AppService;
            this.Repository = Repository;
            this.RegionManager = RegionManager;
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

                        var param = new NavigationParameters();

                        param.Add(nameof(ProjectRoot.ModuleInformation), Data.ModuleInformation);
                        param.Add(nameof(ProjectRoot.ModuleConfiguration), Data.ModuleConfiguration);

                        this.RegionManager.RequestNavigate(Names.MainContentRegion, "InfoEditorView", param);

                        var views = this.RegionManager.Regions[Names.TopRegion].Views;
                        foreach (var item in views)
                        {
                            this.RegionManager.Regions[Names.TopRegion].Remove(item);
                        }
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
