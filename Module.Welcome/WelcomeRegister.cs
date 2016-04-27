using FomodInfrastructure;
using Module.Welcome.View;
using Module.Welcome.ViewModel;
using Prism.Modularity;
using Prism.Regions;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleRegister
{
    public class WelcomeRegister : IModule
    {
        private readonly IRegionManager RegionManager;
        private readonly IContainer Container;

        public WelcomeRegister(IRegionManager RegionManager, IContainer Container)
        {
            this.RegionManager = RegionManager;
            this.Container = Container;
        }


        public void Initialize()
        {
            Container.Configure(r =>
            {
                r.For<object>().Use<WelcomeView>().Named(nameof(WelcomeView)).SetProperty(p => p.DataContext = Container.GetInstance<WelcomeViewModel>());
            });

            this.RegionManager.Regions[Names.TopRegion].RequestNavigate(nameof(WelcomeView));

        }
    }

}
