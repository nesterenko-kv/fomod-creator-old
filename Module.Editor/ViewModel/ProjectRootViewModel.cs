using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base;
using FomodModel.Base.ModuleCofiguration;

namespace Module.Editor.ViewModel
{
    public class ProjectRootViewModel : BaseViewModel
    {
        //AddImage

        public RelayCommand<bool> AddImage { get; }


        public ProjectRootViewModel()
        {
            AddImage = new RelayCommand<bool>(p =>
            {
                var pr = (this.Data as ProjectRoot);
                pr.ModuleConfiguration.ModuleImage = new HeaderImage
                {
                    Path = "aaaa/dsdsd/dasd",
                    ShowFade = false,
                    ShowImage = true
                };
            });
        }
    }
}