namespace FOMOD.Creator
{
    using System.Windows;
    using FOMOD.Creator.Boot;

    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var boot = new Bootstrapper();
            boot.Run();
        }
    }
}
