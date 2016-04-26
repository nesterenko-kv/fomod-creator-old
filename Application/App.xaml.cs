using System.Windows;
using MainApplication.Boot;

namespace MainApplication
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var boot = new Bootstrapper();
            boot.Run();
        }
    }
}
