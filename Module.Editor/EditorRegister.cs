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
        public EditorRegister(IContainer container)
        {
            _container = container;
        }

        #region Services

        private readonly IContainer _container;

        #endregion

        #region IModule

        public void Initialize()
        {
            Registry<MainEditorView, MainEditorViewModel>();
            Registry<ProjectRootView, ProjectRootViewModel>();
            Registry<InstallStepView, InstallStepViewModel>();
            Registry<GroupView, GroupViewModel>();
            Registry<PluginView, PluginViewModel>();
        }

        #endregion

        private void Registry<TView, TViewmodel>() where TView : FrameworkElement
        {
            var name = typeof (TView).Name;
            _container.Configure(r => r.For<object>()
                                       .Use<TView>()
                                       .Named(name)
                                       .SetProperty(p => p.DataContext = _container.GetInstance<TViewmodel>()));
        }

    }
}