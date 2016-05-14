using System.Windows;
using FomodInfrastructure.Interface;
using FomodModel.Base;
using MahApps.Metro.Controls.Dialogs;
using MainApplication.Services;
using Module.Editor;
using Module.Welcome;
using Prism.Logging;
using Prism.StructureMap;
using System.Windows.Markup;

namespace MainApplication.Boot
{
    internal class Bootstrapper : StructureMapBootstrapper
    {
        #region StructureMapBootstrapper

        protected override DependencyObject CreateShell()
        {
            var shell = Container.GetInstance<Shell>();
            var vm = Container.GetInstance<ShellViewModel>();
            (shell as FrameworkElement) .DataContext = vm;
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
                r.For<IComponentConnector>().OnCreationForAll(s => s.InitializeComponent());
                r.For<IAppService>().Use<AppService>().Singleton();
                r.For<IRepository<ProjectRoot>>().Use<Repository>();
                r.For<IDataService>().Use<DataService>().Singleton();
                r.For<IDialogCoordinator>().Use<DialogCoordinator>().Singleton();
                r.For<IFolderBrowserDialog>().Use<FolderBrowserDialog>().Singleton();
                r.For<IFileBrowserDialog>().Use<FileBrowserDialog>();
                r.For<IMemoryService>().Use<MemoryService>();
            });
        }

        protected override ILoggerFacade CreateLogger() => new Logger();

        protected override void InitializeModules()
        {
            Container.GetInstance<WelcomeRegister>().Initialize();
            Container.GetInstance<EditorRegister>().Initialize();
        }
        
        #endregion
    }
}