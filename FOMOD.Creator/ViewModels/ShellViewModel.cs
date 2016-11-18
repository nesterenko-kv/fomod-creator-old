namespace FOMOD.Creator.ViewModels
{
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Input;
    using FOMOD.Creator.Commands;
    using FOMOD.Creator.Interfaces;
    using FOMOD.Creator.PrismEvent;
    using MahApps.Metro.Controls.Dialogs;
    using Prism.Events;
    using Prism.Regions;
    using PropertyChanged;
    using StructureMap;

    [ImplementPropertyChanged]
    public class ShellViewModel : ProjectWorkerBaseViewModel
    {
        private static readonly string DefaultTitle = $"FOMOD Creator beta v{Assembly.GetExecutingAssembly().GetName().Version}";

        private ICommand _closeTabCommand;
        private ICommand _dropFolderCommand;
        private ICommand _saveProjectAsCommand;
        private ICommand _saveProjectCommand;

        public ShellViewModel(IRegionManager regionManager, IDialogCoordinator dialogCoordinator, IContainer container, IEventAggregator eventAggregator)
            : base(dialogCoordinator, container, eventAggregator, regionManager)
        {
        }

        public ICommand CloseTabCommand
        {
            get
            {
                return _closeTabCommand ?? (_closeTabCommand = new RelayCommand<object>(CloseTab));
            }
        }

        public ICommand DropFolderCommand
        {
            get
            {
                return _dropFolderCommand ?? (_dropFolderCommand = new RelayCommand<IDataObject>(OnDropItem, AcceptDrop));
            }
        }

        public ICommand SaveProjectAsCommand
        {
            get
            {
                return _saveProjectAsCommand ?? (_saveProjectAsCommand = new RelayCommand(SaveProjectAs, CanSaveProject));
            }
        }

        public ICommand SaveProjectCommand
        {
            get
            {
                return _saveProjectCommand ?? (_saveProjectCommand = new RelayCommand(SaveProject, CanSaveProject));
            }
        }

        public FrameworkElement SelectedItem { get; set; }

        [DependsOn(nameof(SelectedItem))]
        public string Title
        {
            get
            {
                var viewModel = SelectedItem?.DataContext as IHaveDisplayName;
                return viewModel != null
                    ? $"{viewModel.DisplayName}: {DefaultTitle}"
                    : DefaultTitle;
            }
        }

        public void OnSelectedItemChanged()
        {
            ((RelayCommand) SaveProjectCommand).RaiseCanExecuteChanged();
            ((RelayCommand) SaveProjectAsCommand).RaiseCanExecuteChanged();
        }

        private static bool AcceptDrop(IDataObject data)
        {
            return data != null && data.GetDataPresent(DataFormats.FileDrop);
        }

        private bool CanSaveProject()
        {
            return SelectedItem?.DataContext is EditorViewModel;
        }

        private async void CloseTab(object p)
        {
            var model = p as ICloseable;
            if (model == null)
                return;
            var removeView = RegionManager.Regions[Names.MainContentRegion].Views.Cast<FrameworkElement>().FirstOrDefault(v => v.DataContext == p);
            if (removeView == null)
                return;
            var needSave = model.IsNeedSave;
            if (needSave)
            {
                var result = await DialogCoordinator.ShowMessageAsync(this, "Close", "Save project before closing?", MessageDialogStyle.AffirmativeAndNegative, Container.GetInstance<MetroDialogSettings>());
                if (result == MessageDialogResult.Affirmative)
                    SaveProject();
            }
            removeView.DataContext = null;
            RegionManager.Regions[Names.MainContentRegion].Remove(removeView);
            model.Close();
        }

        private void OnDropItem(IDataObject data)
        {
            var filePath = data.GetData(DataFormats.FileDrop) as string[];
            if (filePath == null)
                return;
            foreach (var path in filePath.Where(Directory.Exists))
                EventAggregator.GetEvent<OpenLink>().Publish(path);
        }

        private void SaveProject()
        {
            if (!CanSaveProject())
                return;
            var viewModel = (EditorViewModel) SelectedItem.DataContext;
            viewModel.IsNeedSave = false;
            viewModel.Save();
            EventAggregator.GetEvent<OpenProjectEvent>().Publish(viewModel.Data);
        }

        private void SaveProjectAs()
        {
            if (!CanSaveProject())
                return;
            var viewModel = (EditorViewModel) SelectedItem.DataContext;
            viewModel.IsNeedSave = false;
            viewModel.SaveAs();
            EventAggregator.GetEvent<OpenProjectEvent>().Publish(viewModel.Data);
        }
    }
}
