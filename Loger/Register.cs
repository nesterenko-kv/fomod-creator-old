using FomodInfrastructure;

using Prism.Modularity;
using Prism.Regions;
using StructureMap;

namespace Loger
{
    public class Register : IModule
    {
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
                    .Use<view>()
                    .Named(nameof(view))
                    .SetProperty(p => p.DataContext = _container.GetInstance<viewmodel>());
            });
            _regionManager.Regions[Names.LogerRegion].RequestNavigate(nameof(view));
        }

        #endregion

        public Register(IRegionManager regionManager, IContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }
    }
}