using FomodInfrastructure;
using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Module.UserMsg.ViewModel
{
    public class SimpleMsgViewModel: BindableBase, INavigationAware
    {
        private readonly IUserMsgService _userMsgService;
        private readonly IRegionManager _regionManager;


        public SimpleMsgViewModel(IUserMsgService userMsgService, IRegionManager regionManager)
        {
            _userMsgService = userMsgService;
            _regionManager = regionManager;
        }

        #region INavigationAware
        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext) { }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var p = navigationContext.Parameters["Msg"].ToString();
            if (!string.IsNullOrWhiteSpace(p))
            {
                Msg = p;
                OnPropertyChanged(nameof(Msg));
                return;
            }

            throw new NullReferenceException("В параметрах отсутствует сообщение или оно пустое");
        }
        #endregion


        //TO DO - сделать что бы сообщения накапливались на случай если поочередно их кто то отправляет
        ICommand _ok;

        public string Msg { get; private set; }

        public ICommand Ok
        {
            get
            {
                if (_ok == null)
                    _ok = new RelayCommand(p =>
                    {
                        var tegion = _regionManager.Regions[Names.UserMsgRegion];
                        foreach (var item in tegion.Views)
                            tegion.Remove(item);
                    });
                return _ok;
            }
        }
    }
}
