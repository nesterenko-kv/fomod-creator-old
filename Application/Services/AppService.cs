using System.Linq;
using System.Windows;
using FomodInfrastructure;
using FomodInfrastructure.Interface;
using FomodModel.Base;
using Microsoft.Practices.ServiceLocation;
using Module.Editor.View;
using Module.Editor.ViewModel;
using Prism.Regions;

namespace MainApplication.Services
{
    public class AppService : IAppService
    {
        #region Services

        private readonly IRegionManager _regionManager;
        private readonly IServiceLocator _serviceLocator;

        #endregion

        #region IAppService

        public void CloseApp()
        {
            Application.Current.MainWindow.Close();
        }

        public void InitilizeBaseModules()
        {

        }

        public void CreateEditorModule<T>(IRepository<T> repository)
        {
            foreach (FrameworkElement element in _regionManager.Regions[Names.MainContentRegion].Views)
            {
                if (!(element is MainEditorView)) continue;
                var b = (element.DataContext as MainEditorViewModel)?.Data.FirstOrDefault(i => i.FolderPath == repository.CurrentPath);
                if (b == null) continue;
                _regionManager.Regions[Names.MainContentRegion].Activate(element);
                return;
            }

            var view = _serviceLocator.GetInstance<object>(nameof(MainEditorView)) as FrameworkElement;
            var detailsRegion = _regionManager.Regions[Names.MainContentRegion];
            var detailsRegionManager = detailsRegion.Add(view, null, true);
            //(view.DataContext as EditorViewModel).ConfigurateViewModel(detailsRegionManager, repository.GetData() as ProjectRoot);
            (view.DataContext as MainEditorViewModel).ConfigurateViewModel(detailsRegionManager, repository as IRepository<ProjectRoot>);
            detailsRegion.Activate(view);
        }

        #endregion

        public AppService(IServiceLocator serviceLocator, IRegionManager regionManager)
        {
            _serviceLocator = serviceLocator;
            _regionManager = regionManager;
        }

    }
}