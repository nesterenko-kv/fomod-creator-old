using Module.Editor.View;
using Module.Editor.ViewModel;
using Prism.Modularity;
using Prism.Regions;
using StructureMap;

namespace Module.Editor
{
    public class EditorRegister : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IContainer _container;

        public EditorRegister(IRegionManager regionManager, IContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }


        public void Initialize()
        {
            _container.Configure(r =>
            {
                r.For<object>().Use<MainEditorView>().Named(nameof(MainEditorView)).SetProperty(p => p.DataContext = _container.GetInstance<MainEditorViewModel>());
            });
        }
    }

}
