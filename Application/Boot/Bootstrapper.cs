using System.Windows;
using System.Windows.Markup;
using FomodInfrastructure.Interface;
using FomodModel.Base;
using Module.Loger;
using MahApps.Metro.Controls.Dialogs;
using MainApplication.Services;
using Module.Editor;
using Module.Welcome;
using Prism.StructureMap;

namespace MainApplication.Boot
{
    internal class Bootstrapper : StructureMapBootstrapper
    {
        #region StructureMapBootstrapper

        protected override DependencyObject CreateShell()
        {
            var shell = Container.GetInstance<Shell>();
            var vm = Container.GetInstance<ShellViewModel>();
            shell.DataContext = vm;
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
                r.For<IComponentConnector>().OnCreationForAll(s => s.InitializeComponent());
                r.For<IAppService>().Use<AppService>().Singleton();
                r.For<IRepository<ProjectRoot>>().Use<Repository>();
                r.For<IDataService>().Use<DataService>().Singleton();
                r.For<IDialogCoordinator>().Use<DialogCoordinator>().Singleton();
                r.For<IFolderBrowserDialog>().Use<FolderBrowserDialog>().Singleton();
                r.For<IFileBrowserDialog>().Use<FileBrowserDialog>();
                r.For<IMemoryService>().Use<MemoryService>();
                r.For<ILogger>().Use<Logger>().Singleton();
                r.ForConcreteType<MetroDialogSettings>().Configure
                    .Ctor<string>("AffirmativeButtonText").Is("ЕПТЫ БЛЯ")
                    .Ctor<string>("NegativeButtonText").Is("НЕТ ТЫ ЧЕ"); //TODO: Localize
            });
        }

        ////protected override ILoggerFacade CreateLogger() => this.Container.GetInstance<Logger>();

        protected override void InitializeModules()
        {
            var welcome = Container.GetInstance<WelcomeRegister>() as WelcomeRegister;

            Container.GetInstance<Register>().Initialize();
            welcome.Initialize();
            Container.GetInstance<EditorRegister>().Initialize();

            welcome.OpenProjectsFromCommandLine();

            ////Container.GetInstance<WelcomeNew.Register>().Initialize();
            ////Container.GetInstance<EditorNew.Register>().Initialize();
        }

        #endregion
    }
}