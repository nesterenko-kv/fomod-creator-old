using FomodInfrastructure;
using FomodInfrastructure.Interface;
using Module.UserMsg.View;
using Module.UserMsg.ViewModel;
using Prism.Modularity;
using Prism.Regions;
using StructureMap;

namespace Module.Welcome
{
    public class UserMsgRegister : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IContainer _container;
        private readonly IUserMsgService _userMsgService;


        public UserMsgRegister(IRegionManager regionManager, IContainer container, IUserMsgService userMsgService)
        {
            _regionManager = regionManager;
            _container = container;
            _userMsgService = userMsgService;
        }


        public void Initialize()
        {
            _container.Configure(r =>
            {
                r.For<object>().Use<SimpleMsgView>().Named(nameof(SimpleMsgView)).SetProperty(p => p.DataContext = _container.GetInstance<SimpleMsgViewModel>());
            });


            _userMsgService.ShowMsg += (args) =>
            {
                var param = new NavigationParameters();
                param.Add("Msg", args.Msg);
                _regionManager.RequestNavigate(Names.UserMsgRegion, nameof(SimpleMsgView), param);
            };
        }
    }

}
