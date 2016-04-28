using FomodModel.Base;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.InfoEditor.ViewModel
{
    public class InfoEditorViewModel: BindableBase, INavigationAware
    {
        #region Fields

        string _header = "ProjectInfo";
        ModuleInformation _moduleInformation; 

        #endregion



        public string Header
        {
            get
            {
                return _header;
            }
            set
            {
                _header = value; OnPropertyChanged(nameof(Header));
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
                _moduleInformation = value; OnPropertyChanged(nameof(ModuleInformation));
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
            if (p != null)
            {
                this.ModuleInformation = p;
                this.Header = "ProjectInfo: " + p.Name;
                return;
            }

            throw new NullReferenceException("В параметрах отсутствует нужный тип данных");
        } 

        #endregion
    }
}
