using System.Windows;
using FomodInfrastructure.Interface;
using Microsoft.Practices.ServiceLocation;
using Module.Editor;

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
            Application.Current.MainWindow.Close();
        }

        public void InitilizeBaseModules()
        {
            _serviceLocator.GetInstance<EditorRegister>().Initialize();
        }
    }
}
