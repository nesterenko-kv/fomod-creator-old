using FomodModel.Base;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace Module.InfoEditor.ViewModel
{
    public class InfoEditorViewModel: BindableBase, INavigationAware
    {
        #region Fields

        private string _header = "ProjectInfo";
        private ModuleInformation _moduleInformation; 

        #endregion

        public string Header
        {
            get
            {
                return _header;
            }
            set
            {
                SetProperty(ref _header, value);
            }
        }
        public ModuleInformation ModuleInformation
        {
            get
            {
                return _moduleInformation;
            }
            set
            {
                SetProperty(ref _moduleInformation, value);
            }
        }

        public InfoEditorViewModel()
        {
            
        }
        #region INavigationAware

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
           
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var p = navigationContext.Parameters[nameof(ProjectRoot.ModuleInformation)] as ModuleInformation;
            if (p == null) throw new NullReferenceException("В параметрах отсутствует нужный тип данных");
            ModuleInformation = p;
            Header = "ProjectInfo: " + p.Name;
        } 
        #endregion
    }
}
