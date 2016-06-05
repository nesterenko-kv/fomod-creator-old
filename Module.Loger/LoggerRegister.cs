using FomodInfrastructure;
using Module.Loger.ViewModel;
using Prism.Modularity;
using Prism.Regions;
using StructureMap;

namespace Module.Loger
{
    public class LoggerRegister : IModule
    {
        public LoggerRegister(IRegionManager regionManager, IContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        #region IModule

        public void Initialize()
        {
            _container.Configure(r => r.For<object>().Use<View.LogerView>().Named(nameof(View.LogerView)).SetProperty(p => p.DataContext = _container.GetInstance<LogerViewModel>()));
            _regionManager.Regions[Names.LogerRegion].RequestNavigate(nameof(View.LogerView));
        }

        #endregion

        #region Services

        private readonly IRegionManager _regionManager;

        private readonly IContainer _container;

        #endregion
    }
}