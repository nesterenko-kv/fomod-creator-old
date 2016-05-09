using FomodInfrastructure;
using Module.Editor.View;
using Module.Editor.ViewModel;
using Prism.Modularity;
using Prism.Regions;
using StructureMap;
using System.Windows;

namespace Module.Editor
{
    public class EditorRegister : IModule
    {
        #region Services

        private readonly IContainer _container;
        private readonly IRegionManager _regionManager;

        #endregion

        #region IModule

        public void Initialize()
        {
            Registry<MainEditorView, EditorViewModel>();
            Registry<ProjectRootView, NullViewModel>();
            Registry<ConfigView, NullViewModel>();
            Registry<GroupView, NullViewModel>();
            Registry<InstallStepView, NullViewModel>();
            Registry<View.Plugin.PluginView, NullViewModel>();
            _regionManager.RequestNavigate(Names.MainContentRegion, nameof(MainEditorView));
        }

        #endregion

        public EditorRegister(IContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        private void Registry<TView, TViewmodel>() where TView: FrameworkElement
        {
            var name = typeof(TView).Name;
            _container.Configure(r => r.For<object>().Use<TView>().Named(name).SetProperty(p => p.DataContext = _container.GetInstance<TViewmodel>()));

        }
    }

}
