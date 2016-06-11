using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AspectInjector.Broker;
using FomodInfrastructure;
using FomodInfrastructure.MvvmLibrary.Commands;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.ServiceLocation;
using Module.Editor.ViewModel;
using Module.Welcome.PrismEvent;
using Prism.Events;
using Prism.Regions;
using System.IO;
using FomodInfrastructure.Aspects;
using FomodInfrastructure.Interfaces;
using Module.Welcome.ViewModel;

namespace MainApplication
{
    public class ShellViewModel : ProjectWorkerBaseViewModel
    {
        public ShellViewModel(IRegionManager regionManager, IAppService appService, IDialogCoordinator dialogCoordinator, IEventAggregator eventAggregator, IServiceLocator serviceLocator, IFolderBrowserDialog folderBrowserDialog)
            : base(eventAggregator, dialogCoordinator, serviceLocator, appService, folderBrowserDialog)
        {
            Title = _defaultTitle = $"FOMOD Creator beta v{appService.Version}";
            _regionManager = regionManager;
            SaveProjectCommand = new RelayCommand(SaveProject, CanSaveProject);
            SaveProjectAsCommand = new RelayCommand(SaveProjectAs, CanSaveProject);
        }

        #region Services

        private readonly IRegionManager _regionManager;

        #endregion

        #region Commands

        private ICommand _closeTabCommand;
        
        public ICommand CloseTabCommand
        {
            get { return _closeTabCommand ?? (_closeTabCommand = new RelayCommand<object>(CloseTab)); }
        }

        private ICommand _dropFolderCommand;
        public ICommand DropFolderCommand
        {
            get { return _dropFolderCommand ?? (_dropFolderCommand = new RelayCommand<IDataObject>(OnDropItem, AcceptDrop)); }
        }

        public RelayCommand SaveProjectCommand { get; }

        public RelayCommand SaveProjectAsCommand { get; }

        #endregion

        #region Properties

        private readonly string _defaultTitle;

        private object _curentSelectedItem;

        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public string Title { get; set; }

        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public object CurentSelectedItem
        {
            get { return _curentSelectedItem; }
            set
            {
                _curentSelectedItem = value;
                SaveProjectCommand.RaiseCanExecuteChanged();
                SaveProjectAsCommand.RaiseCanExecuteChanged();
                var b = (CurentSelectedItem as FrameworkElement)?.DataContext;
                if (b is MainEditorViewModel)
                    Title = $"{(b as MainEditorViewModel).FirstData.ModuleInformation.Name}: {_defaultTitle}";
                else
                    Title = _defaultTitle;
            }
        }

        #endregion

        #region Methods

        private async void CloseTab(object p)
        {
            if (!(p is MainEditorViewModel))
                return;
            var removeView = _regionManager.Regions[Names.MainContentRegion].Views.Cast<FrameworkElement>().FirstOrDefault(v => v.DataContext == p);
            if (removeView == null)
                return;
            var needSave = ((MainEditorViewModel)p).IsNeedSave;
            if (needSave)
            {
                var result = await CofirmDialogAsync();
                if (result)
                    SaveProject();
            }
            removeView.DataContext = null;
            _regionManager.Regions[Names.MainContentRegion].Remove(removeView);
            ((MainEditorViewModel)p).Dispose();
            // ReSharper disable once RedundantAssignment
            removeView = null;
            GC.Collect();
        }

        private void SaveProject()
        {
            var vm = (MainEditorViewModel)((FrameworkElement)CurentSelectedItem).DataContext;
            vm.IsNeedSave = false;
            vm.Save();
            foreach (var projectRoot in vm.Data)
                EventAggregator.GetEvent<OpenProjectEvent>().Publish(projectRoot);
        }

        private void SaveProjectAs()
        {
            var vm = (MainEditorViewModel)((FrameworkElement)CurentSelectedItem).DataContext;
            vm.IsNeedSave = false;
            vm.SaveAs();
            foreach (var projectRoot in vm.Data)
                EventAggregator.GetEvent<OpenProjectEvent>().Publish(projectRoot);
        }

        private bool CanSaveProject()
        {
            return (CurentSelectedItem as FrameworkElement)?.DataContext is MainEditorViewModel;
        }

        private async Task<bool> CofirmDialogAsync() //TODO: Localize
        {
            return await DialogCoordinator.ShowMessageAsync(this, "Close", "Save project before closing?", MessageDialogStyle.AffirmativeAndNegative, ServiceLocator.GetInstance<MetroDialogSettings>()) == MessageDialogResult.Affirmative;
        }

        private bool AcceptDrop(IDataObject data)
        {
            return data != null && data.GetDataPresent(DataFormats.FileDrop);
        }

        private void OnDropItem(IDataObject data)
        {
            var filePath = (string[])data.GetData(DataFormats.FileDrop);
            foreach (var path in filePath.Where(Directory.Exists))
                EventAggregator.GetEvent<OpenLink>().Publish(path);
        }

        #endregion
    }
}