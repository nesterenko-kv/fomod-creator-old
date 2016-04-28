using System;
using FomodInfrastructure.Interface;
using Microsoft.Practices.ServiceLocation;
using Module.Welcome;

namespace MainApplication.Services
{
    public class AppService : IAppService
    {
        private readonly IServiceLocator _serviceLocator;

        public AppService(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }


        public void CloseApp()
        {
            App.Current.MainWindow.Close();
        }

        public void InitilizeBaseModules()
        {
            _serviceLocator.GetInstance<InfoEditorRegister>().Initialize();
        }
    }
}
