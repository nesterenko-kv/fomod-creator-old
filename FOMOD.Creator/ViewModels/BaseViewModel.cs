namespace FOMOD.Creator.ViewModels
{
    using System;
    using Prism.Regions;
    using PropertyChanged;

    [ImplementPropertyChanged]
    public abstract class BaseViewModel<T> : INavigationAware where T : class
    {
        private static readonly string TypeName = typeof(T).Name;

        public T Data { get; set; }

        public string FolderPath { get; set; }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return navigationContext.Parameters[TypeName] == Data;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (!(navigationContext.Parameters[TypeName] is T))
                throw new ArgumentNullException(nameof(navigationContext));
            Data = (T) navigationContext.Parameters[TypeName];
            FolderPath = navigationContext.Parameters["FolderPath"] as string;
        }
    }
}
