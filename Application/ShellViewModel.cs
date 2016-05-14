using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FomodInfrastructure;
using FomodInfrastructure.MvvmLibrary.Commands;
using Prism.Regions;
using FomodInfrastructure.Aspect;
using AspectInjector.Broker;
using MahApps.Metro.Controls.Dialogs;
using Module.Editor.ViewModel;
using System.Diagnostics;
using System.Reflection;

namespace MainApplication
{
    public class ShellViewModel
    {
        private readonly string _defautlTitle;

        #region Services

        private readonly IRegionManager _regionManager;
        private readonly IDialogCoordinator _dialogCoordinator;

        #endregion

        public ShellViewModel(IRegionManager regionManager, IDialogCoordinator dialogCoordinator)
        {
            Title = _defautlTitle = "FOMOD Creator beta v" + GetVersion();
            _regionManager = regionManager;
            _dialogCoordinator = dialogCoordinator;
            CloseTabCommand = new RelayCommand<object>(CloseTab);
            SaveProjectCommand = new RelayCommand(SaveProject, CanSaveProject);
            SaveProjectAsCommand = new RelayCommand(SaveProjectAs, CanSaveProject);
        }

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
            if (!(p is MainEditorViewModel)) return;
            var removeView = _regionManager.Regions[Names.MainContentRegion].Views.Cast<FrameworkElement>().FirstOrDefault(v => v.DataContext == p);
            if (removeView == null) return;
            var needSave = ((MainEditorViewModel)p).IsNeedSave;
            if (needSave)
            {
                var result = await CofirmDialog();
                if (result)
                    SaveProject();
            }
            _regionManager.Regions[Names.MainContentRegion].Remove(removeView);
        }

        private void SaveProject()
        {
            var vm = (MainEditorViewModel) ((FrameworkElement) CurentSelectedItem).DataContext;
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

        private async Task<bool> CofirmDialog() => await _dialogCoordinator.ShowMessageAsync(this, "Закрыть проект", "Сохранить перед закрытием?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative;
        
        private string GetVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }

        #endregion
    }
}