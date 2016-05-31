using System;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using Prism.Regions;

namespace EditorNew.ViewModel
{
    public class BaseViewModel<T> : INavigationAware where T: class
    {
        private readonly string _curentParamName;

        public BaseViewModel()
        {
            _curentParamName = GetType().Name.Replace("ViewModel", string.Empty);
        }

        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public T Data { get; set; }

        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public string FolderPath { get; set; }

        #region INavigationAware

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var param = navigationContext.Parameters[_curentParamName] as T;
            if (param != null)
                Data = param;
            FolderPath = (string)navigationContext.Parameters["FolderPath"];
            if (Data == null)
                throw new ArgumentNullException(nameof(navigationContext), "При навигации обязательныо нужно передавать параметры");
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