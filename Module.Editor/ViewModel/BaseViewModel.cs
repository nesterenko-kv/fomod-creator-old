using System;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using Prism.Regions;

namespace Module.Editor.ViewModel
{
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    public abstract class BaseViewModel<T> : INavigationAware where T : class
    {
        private readonly string _curentParamName;

        protected BaseViewModel()
        {
            _curentParamName = GetType().Name.Replace("ViewModel", string.Empty);
        }

        public T Data { get; set; }

        public string FolderPath { get; set; }

        #region INavigationAware

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Data = navigationContext.Parameters[_curentParamName] as T;
            if (Data == null)
                throw new ArgumentNullException(nameof(navigationContext), "When navigating necessarily need to pass parameters"); //TODO: Localize
            FolderPath = navigationContext.Parameters["FolderPath"] as string;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return navigationContext.Parameters[_curentParamName] == Data;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext) {}

        #endregion
    }
}