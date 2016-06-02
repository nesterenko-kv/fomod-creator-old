using FomodInfrastructure;
using Prism.Modularity;
using Prism.Regions;
using StructureMap;

namespace Loger
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
            _container.Configure(r => r.For<object>().Use<LogerView>().Named(nameof(LogerView)).SetProperty(p => p.DataContext = _container.GetInstance<LogerViewModel>()));
            _regionManager.Regions[Names.LogerRegion].RequestNavigate(nameof(LogerView));
        }

        #endregion

        #region Services

        private readonly IRegionManager _regionManager;

        private readonly IContainer _container;

        #endregion
    }
}