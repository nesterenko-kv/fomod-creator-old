using System.Windows;
using FomodInfrastructure.Interface;
using Microsoft.Practices.ServiceLocation;
using Module.Editor;
using FomodInfrastructure;
using Prism.Regions;
using Module.Editor.View;
using Module.Editor.ViewModel;
using System.Linq;
using FomodModel.Base;

namespace MainApplication.Services
{
    public class AppService : IAppService
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly IRegionManager _regionManager;

        public AppService(IServiceLocator serviceLocator, IRegionManager regionManager)
        {
            _serviceLocator = serviceLocator;
            _regionManager = regionManager;
        }

        public void CloseApp()
        {
            Application.Current.MainWindow.Close();
        }

        public void InitilizeBaseModules()
        {
            
        }


        public void CreateEditorModule<T>(IRepository<T> repository)
        {
            ProjectRoot b = null;
            foreach (FrameworkElement v in _regionManager.Regions[Names.MainContentRegion].Views)
            {
                if (!(v is MainEditorView)) continue;
                b = (v.DataContext as EditorViewModel).Data.FirstOrDefault(i => i.FolderPath == repository.CurrentPath); 
                if (b!=null)
                {
                    _regionManager.Regions[Names.MainContentRegion].Activate(v);
                    return;
                }
            }

            IRegion detailsRegion = _regionManager.Regions[Names.MainContentRegion];
            var view = _serviceLocator.GetInstance<object>(nameof(MainEditorView)) as FrameworkElement;
            IRegionManager detailsRegionManager = detailsRegion.Add(view, null, true);
            (view.DataContext as EditorViewModel).ConfigurateViewModel(detailsRegionManager, repository.GetData() as ProjectRoot);

            _regionManager.Regions[Names.MainContentRegion].Activate(view);

        }
    }
}