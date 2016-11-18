namespace FOMOD.Creator.ViewModels
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using System.Windows.Input;
    using FOMOD.Creator.Commands;
    using FOMOD.Creator.Interfaces;
    using FOMOD.Creator.PrismEvent;
    using FOMOD.Creator.Views;
    using MahApps.Metro.Controls.Dialogs;
    using Prism.Events;
    using Prism.Regions;
    using StructureMap;
    using Application = System.Windows.Application;

    public abstract class ProjectWorkerBaseViewModel
    {
        protected readonly IContainer Container;
        protected readonly IDialogCoordinator DialogCoordinator;
        protected readonly IEventAggregator EventAggregator;
        protected readonly IRegionManager RegionManager;

        private ICommand _closeApplicationCommand;
        private ICommand _createProjectCommand;
        private ICommand _openProjectCommand;

        protected ProjectWorkerBaseViewModel(IDialogCoordinator dialogCoordinator, IContainer container, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            DialogCoordinator = dialogCoordinator;
            Container = container;
            EventAggregator = eventAggregator;
            RegionManager = regionManager;
        }

        public ICommand CloseApplicationCommand
        {
            get
            {
                return _closeApplicationCommand ?? (_closeApplicationCommand = new RelayCommand(Application.Current.MainWindow.Close));
            }
        }

        public ICommand CreateProjectCommand
        {
            get
            {
                return _createProjectCommand ?? (_createProjectCommand = new RelayCommand(CreateProject));
            }
        }

        public ICommand OpenProjectCommand
        {
            get
            {
                return _openProjectCommand ?? (_openProjectCommand = new RelayCommand<string>(OpenProject));
            }
        }


        public void CreateEditor(IProjectManager manager)
        {
            if (manager == null)
                throw new ArgumentNullException(nameof(manager));
            if (!TryActivate(manager))
                CreateEditorInternal(manager);
        }

        public void CreateProject()
        {
            var manager = Container.GetInstance<IProjectManager>();
            if (!TryGetFolderPath(out string path))
                return;
            var result = manager.Create(path);
            if (!result.Success)
                return;
            CreateEditor(manager);
            EventAggregator.GetEvent<OpenProjectEvent>().Publish(result.Data);
        }

        protected void OpenProject(string path = null)
        {
            var manager = Container.GetInstance<IProjectManager>();
            if (!Directory.Exists(path))
                if (!TryGetFolderPath(out path))
                    return;
            var result = manager.Load(path);
            if (!result.Success)
                return;
            CreateEditor(manager);
            EventAggregator.GetEvent<OpenProjectEvent>().Publish(result.Data);
        }

        private static bool TryGetFolderPath(out string path)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            var result = folderBrowserDialog.ShowDialog() == DialogResult.OK && Directory.Exists(folderBrowserDialog.SelectedPath);
            if (result)
                path = folderBrowserDialog.SelectedPath;
            else
                path = string.Empty;
            return result;
        }

        private void CreateEditorInternal(IProjectManager manager)
        {
            var newView = Container.GetInstance<object>(nameof(EditorView)) as EditorView;
            var detailsRegion = RegionManager.Regions[Names.MainContentRegion];
            var detailsRegionManager = detailsRegion.Add(newView, null, true);
            if (newView == null)
                return;
            var newViewModel = newView.ViewModel;
            newViewModel.ConfigureViewModel(detailsRegionManager, manager);
            detailsRegion.Activate(newView);
        }

        private bool TryActivate(IProjectManager manager)
        {
            foreach (var view in RegionManager.Regions[Names.MainContentRegion].Views.OfType<EditorView>())
            {
                var viewModel = view.ViewModel;
                if (!viewModel.Data.Source.Equals(manager.Data.Source, StringComparison.OrdinalIgnoreCase))
                    continue;
                RegionManager.Regions[Names.MainContentRegion].Activate(view);
                return true;
            }
            return false;
        }
    }
}
