using System.Linq;
using FomodInfrastructure;
using FomodInfrastructure.Interface;
using Module.Welcome.PrismEvent;
using Module.Welcome.View;
using Module.Welcome.ViewModel;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using StructureMap;

namespace Module.Welcome
{
    public class WelcomeRegister : IModule
    {
        public WelcomeRegister(IRegionManager regionManager, IContainer container, IAppService appService, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _container = container;
            _appService = appService;
            _eventAggregator = eventAggregator;
        }

        public void OpenProjectsFromCommandLine()
        {
            if (_appService.CommandLineArgs.Length < 1)
                return;
            var commandLineArgs = _appService.CommandLineArgs.Skip(1);
            _appService.IsOpenProjectsFromCommandLine = true;
            try
            {
                foreach (var arg in commandLineArgs)
                    _eventAggregator.GetEvent<OpenLink>().Publish(arg);
            }
            finally
            {
                _appService.IsOpenProjectsFromCommandLine = false;
            }
        }

        #region IModule

        public void Initialize()
        {
            _container.Configure(r =>
            {
                r.For<object>().Use<WelcomeView>().Named(nameof(WelcomeView)).SetProperty(p => p.DataContext = _container.GetInstance<WelcomeViewModel>());
                r.For<object>().Use<LastProjectsView>().Named(nameof(LastProjectsView)).SetProperty(p => p.DataContext = _container.GetInstance<LastProjectsViewModel>());
            });
            _regionManager.Regions[Names.MainContentRegion].RequestNavigate(nameof(WelcomeView));
            _regionManager.Regions[Names.LeftRegion].RequestNavigate(nameof(LastProjectsView));
            OpenProjectsFromCommandLine();
        }

        #endregion

        #region Services

        private readonly IRegionManager _regionManager;
        private readonly IContainer _container;
        private readonly IAppService _appService;
        private readonly IEventAggregator _eventAggregator;

        #endregion
    }
}