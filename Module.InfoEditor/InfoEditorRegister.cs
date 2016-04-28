using Module.InfoEditor.View;
using Module.InfoEditor.ViewModel;
using Prism.Modularity;
using Prism.Regions;
using StructureMap;

namespace Module.InfoEditor
{
    public class InfoEditorRegister : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IContainer _container;

        public InfoEditorRegister(IRegionManager regionManager, IContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }


        public void Initialize()
        {
            _container.Configure(r =>
            {
                r.For<object>().Use<InfoEditorView>().Named(nameof(InfoEditorView)).SetProperty(p => p.DataContext = _container.GetInstance<InfoEditorViewModel>());
            });

            //_regionManager.Regions[Names.TopRegion].Add(_container.GetInstance<object>(nameof(InfoEditorView)));
        }
    }

}
