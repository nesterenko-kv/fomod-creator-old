using System.Linq;
using System.Windows;
using FomodInfrastructure;
using FomodInfrastructure.MvvmLibrary.Commands;
using Prism.Regions;

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
        }

        #region Commands

        public RelayCommand<object> CloseTab { get; }

        #endregion
    }
}