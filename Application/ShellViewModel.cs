using System.Linq;
using System.Windows;
using FomodInfrastructure;
using FomodInfrastructure.MvvmLibrary.Commands;
using Prism.Regions;
using FomodInfrastructure.Aspect;
using AspectInjector.Broker;
using FomodModel.Base;
using Module.Editor.ViewModel;

namespace MainApplication
{
    public class ShellViewModel
    {
        private readonly IRegionManager _regionManager;

        public ShellViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            CloseTab = new RelayCommand<object>(p =>
            {
                var removeView = _regionManager.Regions[Names.MainContentRegion].Views.Cast<FrameworkElement>().FirstOrDefault(v => v.DataContext == p);
                _regionManager.Regions[Names.MainContentRegion].Remove(removeView);
            });
            SaveProject = new RelayCommand<object>(p =>
            {
                var vm = ((MainEditorViewModel)((FrameworkElement)CurentSelectedItem).DataContext);
                vm.Save();
            },
            p => (CurentSelectedItem as FrameworkElement)?.DataContext is MainEditorViewModel);
        }

        #region Commands

        public RelayCommand<object> CloseTab { get; }
        public RelayCommand<object> SaveProject { get; }

        #endregion

        object _CurentSelectedItem;
        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public object CurentSelectedItem
        {
            get
            {
                return _CurentSelectedItem;
            }
            set
            {
                _CurentSelectedItem = value;
                SaveProject.RaiseCanExecuteChanged();
            }
        }
    }
}