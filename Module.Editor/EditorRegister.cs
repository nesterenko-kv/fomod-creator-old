using Module.Editor.View;
using Module.Editor.ViewModel;
using Prism.Modularity;
using StructureMap;
using pluginView = Module.Editor.View.Plugin.pluginView;

namespace Module.Editor
{
    public class EditorRegister : IModule
    {
        #region Services

        private readonly IContainer _container;
        
        #endregion

        public EditorRegister(IContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.Configure(r => r.For<object>().Use<MainEditorView>().Named(nameof(MainEditorView)).SetProperty(p => p.DataContext = _container.GetInstance<EditorViewModel>()));
            _container.Configure(r => r.For<object>().Use<configView>().Named(nameof(configView)).SetProperty(p => p.DataContext = _container.GetInstance<ConfigViewModel>()));
            _container.Configure(r => r.For<object>().Use<installStepView>().Named(nameof(installStepView)).SetProperty(p => p.DataContext = _container.GetInstance<InstallStepViewModel>()));
            _container.Configure(r => r.For<object>().Use<groupView>().Named(nameof(groupView)).SetProperty(p => p.DataContext = _container.GetInstance<GroupViewModel>()));
            _container.Configure(r => r.For<object>().Use<pluginView>().Named(nameof(pluginView)).SetProperty(p => p.DataContext = _container.GetInstance<PluginViewModel>()));
        }
    }

}
