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
        #region Services

        private readonly IRegionManager _regionManager;
        private readonly IContainer _container;

        #endregion

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
                r.For<object>().Use<LastProjectsView>().Named(nameof(LastProjectsView)).SetProperty(p => p.DataContext = _container.GetInstance<LastProjectsViewModel>());
            });
            _regionManager.Regions[Names.TopRegion].RequestNavigate(nameof(WelcomeView));
            _regionManager.Regions[Names.RightRegion].RequestNavigate(nameof(LastProjectsView));

        }
    }

}
