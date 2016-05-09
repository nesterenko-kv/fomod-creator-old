using System.Windows;
using FomodInfrastructure.Interface;
using Microsoft.Practices.ServiceLocation;
using Module.Editor;
using System.Windows.Data;
using Prism.Regions;
using FomodInfrastructure;
using Module.Editor.View;
using Module.Editor.ViewModel;

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
            _serviceLocator.GetInstance<EditorRegister>().Initialize();
        }

        public void CreateEditorModule<T>(IRepository<T> repository)
        {
            bool b = false;
            foreach (FrameworkElement v in _regionManager.Regions[Names.MainContentRegion].Views)
            {
                if (!(v is MainEditorView)) continue;
                b = (v.DataContext as dynamic).ProjectPath == repository.CurrentPath;
                if (b)
                {
                    _regionManager.Regions[Names.MainContentRegion].Activate(v);
                    return;
                }
            }

            IRegion detailsRegion = _regionManager.Regions[Names.MainContentRegion];
            var view = _serviceLocator.GetInstance<MainEditorView>();
            var viewmodel = _serviceLocator.GetInstance<EditorViewModel>();
            viewmodel.XmlData = repository.GetData() as XmlDataProvider;
            viewmodel.ProjectPath = repository.CurrentPath;
            view.DataContext = viewmodel;

            IRegionManager detailsRegionManager = detailsRegion.Add(view, null, true);
            (view.DataContext as dynamic).RegionManager = detailsRegionManager;

            _regionManager.Regions[Names.MainContentRegion].Activate(view);

        }

        public void NavigateToEditor(IRepository<XmlDataProvider> repository)
        {

        }

    }
}
