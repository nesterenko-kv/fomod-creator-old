using EditorNew.View;
using EditorNew.ViewModel;
using Prism.Modularity;
using Prism.Regions;
using StructureMap;

namespace EditorNew
{
    public class Register : IModule
    {
        private Watcher _watcher;

        #region Services

        private readonly IRegionManager _regionManager;
        private readonly IContainer _container;

        #endregion

        #region IModule

        public void Initialize()
        {
            _container.Configure(r =>
            {
                r.For<object>()
                    .Use<MainEditorView>()
                    .Named(nameof(MainEditorView))
                    .SetProperty(p => p.DataContext = _container.GetInstance<MainEditorViewModel>());
            });
            //_regionManager.Regions[Names.MainContentRegion].RequestNavigate(nameof(MainEditorView));
        }

        #endregion

        public Register(IRegionManager regionManager, IContainer container)
        {
            _regionManager = regionManager;
            _container = container;
            _watcher = container.GetInstance<Watcher>();
        }
    }
}