using System.Windows;
using FomodInfrastructure.Interfaces;
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
                r.For<IAppService>().Use<AppService>().Singleton();
                r.For<IRepository<Project>>().Use<ProjectRepository>();
                r.For<IDataService>().Use<DataService>().Singleton();
                r.For<IDialogCoordinator>().Use<DialogCoordinator>().Singleton();
                r.For<IFolderBrowserDialog>().Use<FolderBrowserDialog>().Singleton();
                r.For<IFileBrowserDialog>().Use<FileBrowserDialog>().Singleton();
                r.For<IMemoryService>().Use<MemoryService>();
                r.For<ILogger>().Use<Logger>().Singleton(); //TODO: Localize
                r.ForConcreteType<MetroDialogSettings>().Configure.SetProperty(x=>
                {
                    x.AffirmativeButtonText = "Yes";
                    x.NegativeButtonText = "No";
                });
            });
        }
        
        protected override void InitializeModules()
        {
            Container.GetInstance<LoggerRegister>().Initialize();
            Container.GetInstance<EditorRegister>().Initialize();
            Container.GetInstance<WelcomeRegister>().Initialize();
        }

        #endregion
    }
}