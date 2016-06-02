using FomodInfrastructure;
using Prism.Modularity;
using Prism.Regions;
using StructureMap;

namespace WelcomeNew
{
    public class Register : IModule
    {
        public Register(IRegionManager regionManager, IContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        #region IModule

        public void Initialize()
        {
            _container.Configure(r => { r.For<object>().Use<WelcomeNewView>().Named(nameof(WelcomeNewView)).SetProperty(p => p.DataContext = _container.GetInstance<WelcomeNewViewModel>()); });
            _regionManager.Regions[Names.MainContentRegion].RequestNavigate(nameof(WelcomeNewView));
        }

        #endregion

        #region Services

        private readonly IRegionManager _regionManager;

        private readonly IContainer _container;

        #endregion
    }
}