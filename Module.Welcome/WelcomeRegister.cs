using FomodInfrastructure;
using Module.Welcome.View;
using Module.Welcome.ViewModel;
using Prism.Modularity;
using Prism.Regions;
using StructureMap;

namespace Module.Welcome
{
    public class WelcomeRegister : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IContainer _container;

        public WelcomeRegister(IRegionManager regionManager, IContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }


        public void Initialize()
        {
            _container.Configure(r =>
            {
                r.For<object>().Use<WelcomeView>().Named(nameof(WelcomeView)).SetProperty(p => p.DataContext = _container.GetInstance<WelcomeViewModel>());
            });

            _regionManager.Regions[Names.TopRegion].RequestNavigate(nameof(WelcomeView));

        }
    }

}
