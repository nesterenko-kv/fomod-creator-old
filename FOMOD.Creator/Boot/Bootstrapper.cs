namespace FOMOD.Creator.Boot
{
    using System;
    using System.Linq;
    using System.Windows;
    using FOMOD.Creator.Interfaces;
    using FOMOD.Creator.PrismEvent;
    using FOMOD.Creator.Services;
    using FOMOD.Creator.Views;
    using MahApps.Metro.Controls.Dialogs;
    using Prism.Events;
    using Prism.Regions;
    using Prism.StructureMap;

    public class Bootstrapper : StructureMapBootstrapper
    {
        public override void Run(bool runWithDefaultConfiguration)
        {
            base.Run(runWithDefaultConfiguration);
            NavigateToWelcome();
            OpenCommandLineArgs();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.Configure(r =>
            {
                r.For<IDataService>().Use<DataService>().Singleton();
                r.For<IDialogCoordinator>().Use<DialogCoordinator>().Singleton();
                r.For<IMemoryService>().Use<MemoryService>();
                r.For<IProjectManager>().Use<ProjectManager>();
                r.For<object>().Use<WelcomeView>().Named(nameof(WelcomeView));
                r.For<object>().Use<RecentView>().Named(nameof(RecentView));
                r.For<object>().Use<EditorView>().Named(nameof(EditorView));
                r.For<object>().Use<InstallStepView>().Named(nameof(InstallStepView));
                r.For<object>().Use<ProjectView>().Named(nameof(ProjectView));
                r.For<object>().Use<GroupView>().Named(nameof(GroupView));
                r.For<object>().Use<PluginView>().Named(nameof(PluginView));
                r.ForConcreteType<MetroDialogSettings>().Configure.SetProperty(x =>
                {
                    x.AffirmativeButtonText = "Yes";
                    x.NegativeButtonText = "No";
                });
            });
        }

        protected override DependencyObject CreateShell()
        {
            return Container.GetInstance<ShellView>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (Window) Shell;
            Application.Current.MainWindow.Show();
        }

        private void NavigateToWelcome()
        {
            var regionManager = Container.GetInstance<IRegionManager>();
            regionManager.Regions[Names.MainContentRegion].RequestNavigate(nameof(WelcomeView));
            regionManager.Regions[Names.LeftRegion].RequestNavigate(nameof(RecentView));
        }

        private void OpenCommandLineArgs()
        {
            var eventAggregator = Container.GetInstance<IEventAggregator>();
            var commandLineArgs = Environment.GetCommandLineArgs();
            if (commandLineArgs.Length <= 1)
                return;
            foreach (var arg in commandLineArgs.Skip(1))
                eventAggregator.GetEvent<OpenLink>().Publish(arg);
        }
    }
}
