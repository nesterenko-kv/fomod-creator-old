using System.Windows;
using FomodInfrastructure.Interface;
using FomodModel.Base;
using MainApplication.Services;
using Module.Welcome;
using Prism.Logging;
using Prism.StructureMap;
using System.Xml.Linq;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Data;

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
                r.For<IRepository<XmlDataProvider>>().Use<RepositoryXml>().Singleton();
                r.For<IDialogCoordinator>().Use<DialogCoordinator>().Singleton();
                r.For<IFolderBrowserDialog>().Use<FolderBrowserDialog>().Singleton();
                r.For<IDataService>().Use<DataService>().Singleton();
            });
        }

        protected override ILoggerFacade CreateLogger()
        {
            return new Logger();
        }

        protected override void InitializeModules()
        {
            Container.GetInstance<WelcomeRegister>().Initialize();
        }

    }
}