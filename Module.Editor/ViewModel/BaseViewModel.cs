using System;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using Prism.Mvvm;
using Prism.Regions;

namespace Module.Editor.ViewModel
{
    public class BaseViewModel : BindableBase, INavigationAware
    {
        private readonly string _curentParamName;

        public BaseViewModel()
        {
            _curentParamName = GetType().Name.Replace("ViewModel", "");
        }

        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public object Data { get; set; }

        #region INavigationAware

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Data = navigationContext.Parameters[_curentParamName];
            if (Data == null) throw new ArgumentNullException(nameof(navigationContext), "При навигации обязательныо нужно передавать параметры");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return navigationContext.Parameters[_curentParamName] == Data;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        
        #endregion

    }
}