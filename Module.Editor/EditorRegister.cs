using Module.Editor.View;
using Module.Editor.ViewModel;
using Prism.Modularity;
using Prism.Regions;
using StructureMap;

namespace Module.Editor
{
    public class EditorRegister : IModule
    {
        #region Services

        private readonly IRegionManager _regionManager;
        private readonly IContainer _container;
        
        #endregion

        public EditorRegister(IRegionManager regionManager, IContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            _container.Configure(r => r.For<object>().Use<MainEditorView>().Named(nameof(MainEditorView)).SetProperty(p => p.DataContext = _container.GetInstance<EditorViewModel>()));

            _container.Configure(r => r.For<object>().Use<configView>().Named(nameof(configView)).SetProperty(p => p.DataContext = _container.GetInstance<configViewModel>()));
            _container.Configure(r => r.For<object>().Use<installStepView>().Named(nameof(installStepView)).SetProperty(p => p.DataContext = _container.GetInstance<installStepViewModel>()));
            _container.Configure(r => r.For<object>().Use<groupView>().Named(nameof(groupView)).SetProperty(p => p.DataContext = _container.GetInstance<groupViewModel>()));
            _container.Configure(r => r.For<object>().Use<pluginView>().Named(nameof(pluginView)).SetProperty(p => p.DataContext = _container.GetInstance<pluginViewModel>()));

        }
    }

}
