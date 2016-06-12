using System.Windows;
using Module.Editor.View;
using Module.Editor.ViewModel;
using Prism.Modularity;
using StructureMap;

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

        #region IModule

        public void Initialize()
        {
            Registry<MainEditorView, MainEditorViewModel>();
            Registry<ProjectView, ProjectViewModel>();
            Registry<InstallStepView, InstallStepViewModel>();
            Registry<GroupView, GroupViewModel>();
            Registry<PluginView, PluginViewModel>();
        }

        #endregion

        private void Registry<TView, TViewModel>() where TView : FrameworkElement
        {
            var name = typeof(TView).Name;
            _container.Configure(r => r.For<object>().Use<TView>().Named(name).SetProperty(p => p.DataContext = _container.GetInstance<TViewModel>()));
        }
    }
}