using FomodInfrastructure.Interface;

namespace MainApplication.Services
{
    public class AppService : IAppService
    {
        public void CloseApp()
        {
            App.Current.MainWindow.Close();
        }
    }
}
