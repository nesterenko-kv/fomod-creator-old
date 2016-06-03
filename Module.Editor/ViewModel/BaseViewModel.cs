using System;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using Prism.Regions;

namespace Module.Editor.ViewModel
{
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    public class BaseViewModel<T> : INavigationAware where T : class
    {
        private readonly string _curentParamName;

        public BaseViewModel()
        {
            _curentParamName = GetType().Name.Replace("ViewModel", string.Empty);
        }

        
        public T Data { get; set; }

        public string FolderPath { get; set; }

        #region INavigationAware

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var param = navigationContext.Parameters[_curentParamName] as T;
            if (param != null)
                Data = param;
            FolderPath = (string)navigationContext.Parameters["FolderPath"];
            if (Data == null)
                throw new ArgumentNullException(nameof(navigationContext), "When navigating necessarily need to pass parameters"); //TODO: Localize
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return navigationContext.Parameters[_curentParamName] == Data;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext) {}

        #endregion
    }
}