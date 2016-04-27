using FomodInfrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
