using System.Windows;
using Module.Editor.View;
using Module.Editor.View.Plugin;
using Module.Editor.ViewModel;
using Prism.Modularity;
using Prism.Regions;
using StructureMap;

namespace Module.Editor
{
    public class EditorRegister : IModule
    {
        public EditorRegister(IContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        #region IModule

        public void Initialize()
        {
            Registry<MainEditorView, EditorViewModel>();
            Registry<ProjectRootView, NullViewModel>();
            Registry<ConfigView, NullViewModel>();
            Registry<GroupView, NullViewModel>();
            Registry<InstallStepView, NullViewModel>();
            Registry<PluginView, NullViewModel>();
        }

        #endregion

        private void Registry<TView, TViewmodel>() where TView : FrameworkElement
        {
            var name = typeof (TView).Name;
            _container.Configure(
                r =>
                    r.For<object>()
                        .Use<TView>()
                        .Named(name)
                        .SetProperty(p => p.DataContext = _container.GetInstance<TViewmodel>()));
        }

        #region Services

        private readonly IContainer _container;
        private readonly IRegionManager _regionManager;

        #endregion
    }
}