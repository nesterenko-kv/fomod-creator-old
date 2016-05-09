using FomodInfrastructure;
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
        #region Services

        private readonly IContainer _container;
        private readonly IRegionManager _regionManager;

        #endregion

        #region IModule

        public void Initialize()
        {
            _container.Configure(r => r.For<object>().Use<MainEditorView>().Named(nameof(MainEditorView)).SetProperty(p => p.DataContext = _container.GetInstance<EditorViewModel>()));
            _container.Configure(r => r.For<object>().Use<configView>().Named(nameof(configView)).SetProperty(p => p.DataContext = _container.GetInstance<ConfigViewModel>()));
            _container.Configure(r => r.For<object>().Use<installStepView>().Named(nameof(installStepView)).SetProperty(p => p.DataContext = _container.GetInstance<InstallStepViewModel>()));
            _container.Configure(r => r.For<object>().Use<groupView>().Named(nameof(groupView)).SetProperty(p => p.DataContext = _container.GetInstance<GroupViewModel>()));
            _container.Configure(r => r.For<object>().Use<pluginView>().Named(nameof(pluginView)).SetProperty(p => p.DataContext = _container.GetInstance<PluginViewModel>()));

            //_regionManager.RequestNavigate(Names.MainContentRegion, nameof(MainEditorView));

            //View view = new View();
            //bool createRegionManagerScope = true;
            //IRegion detailsRegion = _regionManager.Regions[Names.MainContentRegion];
            //var view = _container.GetInstance<MainEditorView>();
            //IRegionManager detailsRegionManager = detailsRegion.Add(view, null, true);
            


        }

        #endregion

        public EditorRegister(IContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }
    }

}
