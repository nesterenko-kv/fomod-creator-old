using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AspectInjector.Broker;
using FomodInfrastructure;
using FomodInfrastructure.Aspect;
using FomodInfrastructure.MvvmLibrary.Commands;
using MahApps.Metro.Controls.Dialogs;
using Module.Editor.ViewModel;
using Prism.Regions;

namespace MainApplication
{
    public class ShellViewModel
    {
        private readonly string _defautlTitle;

        public ShellViewModel(IRegionManager regionManager, IDialogCoordinator dialogCoordinator)
        {
            Title = _defautlTitle = $"FOMOD Creator beta v{GetVersion()}";
            _regionManager = regionManager;
            _dialogCoordinator = dialogCoordinator;
            CloseTabCommand = new RelayCommand<object>(CloseTab);
            SaveProjectCommand = new RelayCommand(SaveProject, CanSaveProject);
            SaveProjectAsCommand = new RelayCommand(SaveProjectAs, CanSaveProject);
        }

        #region Services

        private readonly IRegionManager _regionManager;

        private readonly IDialogCoordinator _dialogCoordinator;

        #endregion

        #region Commands

        public RelayCommand<object> CloseTabCommand { get; }

        public RelayCommand SaveProjectCommand { get; }

        public RelayCommand SaveProjectAsCommand { get; }

        #endregion

        #region Properties

        private object _curentSelectedItem;

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
                    Title = $"{(b as MainEditorViewModel).FirstData.ModuleInformation.Name}: {_defautlTitle}";
                else
                    Title = _defautlTitle;
            }
        }

        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public string Title { get; set; }

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

            ////removeView.Finalize();
            ////GC.SuppressFinalize(removeView);
            ((MainEditorViewModel)p).Dispose();
            removeView = null;
            GC.Collect();
        }

        private void SaveProject()
        {
            var vm = (MainEditorViewModel)((FrameworkElement)CurentSelectedItem).DataContext;
            vm.IsNeedSave = false;
            vm.Save();
        }

        private void SaveProjectAs()
        {
            var vm = (MainEditorViewModel)((FrameworkElement)CurentSelectedItem).DataContext;
            vm.IsNeedSave = false;
            vm.SaveAs();
        }

        private bool CanSaveProject() => (CurentSelectedItem as FrameworkElement)?.DataContext is MainEditorViewModel;

        private async Task<bool> CofirmDialogAsync() => await _dialogCoordinator.ShowMessageAsync(this, "Закрыть проект", "Сохранить перед закрытием?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative;

        private string GetVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }

        #endregion
    }
}