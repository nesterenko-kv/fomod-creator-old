using System.Windows;
using Prism.StructureMap;
using MainApplication.Services;
using FomodInfrastructure.Interface;

namespace MainApplication.Boot
{
    internal class Bootstrapper : StructureMapBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            var shell = Container.GetInstance<Shell>();
            return shell;
        }
        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.Configure(r =>
            {
                r.For<IAppService>().Use<AppService>().Singleton();
            });


        }

        protected override void InitializeModules()
        {
            this.Container.GetInstance<ModuleRegister.WelcomeRegister>().Initialize();
        }

    }
}