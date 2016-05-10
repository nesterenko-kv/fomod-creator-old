using FomodInfrastructure;
using FomodInfrastructure.MvvmLibrary.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MainApplication
{
    public class ShellViewModel
    {
        private IRegionManager _regionManager;

        public ShellViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            CloseTab = new RelayCommand<object>(p =>
            {
                FrameworkElement remove_view = null;
                foreach (FrameworkElement v in _regionManager.Regions[Names.MainContentRegion].Views)
                {
                    if (v.DataContext == p)
                    {
                        remove_view = v;
                        break;
                    }
                }

                _regionManager.Regions[Names.MainContentRegion].Remove(remove_view);
            });
        }


        #region Commands

        public RelayCommand<object> CloseTab { get; }

        #endregion
    }
}
