using System.Windows;
using FomodInfrastructure.Interface;
using FomodModel.Base;
using MahApps.Metro.Controls.Dialogs;
using MainApplication.Services;
using Module.Editor;
using Module.Welcome;
using Prism.Logging;
using Prism.StructureMap;

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
            Application.Current.MainWindow = (Window) Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.Configure(r =>
            {
                r.For<IAppService>().Use<AppService>().Singleton();
                r.For<IRepository<ProjectRoot>>().Use<Repository>();
                r.For<IDialogCoordinator>().Use<DialogCoordinator>().Singleton();
                r.For<IFolderBrowserDialog>().Use<FolderBrowserDialog>().Singleton();
                r.For<IDataService>().Use<DataService>().Singleton();
                r.For<IFileBrowserDialog>().Use<FileBrowserDialog>();
            });
        }

        protected override ILoggerFacade CreateLogger() => new Logger();

        protected override void InitializeModules()
        {
            Container.GetInstance<WelcomeRegister>().Initialize();
            Container.GetInstance<EditorRegister>().Initialize();
        }
    }
}