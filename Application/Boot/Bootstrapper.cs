using System.Windows;
using Prism.StructureMap;
using MainApplication.Services;
using FomodInfrastructure.Interface;
using Module.Welcome;
using FomodModel.Base;
using Prism.Logging;

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
                r.For<IRepository<ProjectRoot>>().Use<Repository>().Singleton();
            });


        }

        protected override ILoggerFacade CreateLogger()
        {
            return new Logger();
        }

        protected override void InitializeModules()
        {
            Container.GetInstance<WelcomeRegister>().Initialize();
            Container.GetInstance<InfoEditorRegister>().Initialize();
        }

    }
}