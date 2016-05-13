using System.Linq;
using System.Windows;
using FomodInfrastructure;
using FomodInfrastructure.MvvmLibrary.Commands;
using Prism.Regions;
using FomodInfrastructure.Aspect;
using AspectInjector.Broker;
using FomodModel.Base;
using Module.Editor.ViewModel;
using Module.Welcome.ViewModel;
using System.Diagnostics;

namespace MainApplication
{
    public class ShellViewModel
    {
        private readonly IRegionManager _regionManager;
        private readonly string _defautlTitle;

        public ShellViewModel(IRegionManager regionManager)
        {
            Title = _defautlTitle = "FOMOD Creator beta v" + getVersion();

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
                var b = (CurentSelectedItem as FrameworkElement)?.DataContext;
                if (b!=null && b is MainEditorViewModel)
                    Title = $"{(b as MainEditorViewModel).FirstData.ModuleInformation.Name}: {_defautlTitle}";
                else
                    Title = _defautlTitle;
            }
        }

        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public string Title { get; set; }



        private string getVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }
    }
}