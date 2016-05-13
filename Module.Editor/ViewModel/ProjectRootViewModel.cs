using System;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base;
using FomodModel.Base.ModuleCofiguration;

namespace Module.Editor.ViewModel
{
    public class ProjectRootViewModel : BaseViewModel
    {
        //AddImage

        public RelayCommand AddImageCommand { get; }
        public RelayCommand RemoveImageCommand { get; }

        public ProjectRootViewModel()
        {
            AddImageCommand = new RelayCommand(AddImage);
            RemoveImageCommand = new RelayCommand(RemoveImage);
        }

        private void AddImage()
        {
            var pr = (this.Data as ProjectRoot);
            pr.ModuleConfiguration.ModuleImage = new HeaderImage
            {
                Path = "aaaa/dsdsd/dasd",
                ShowFade = false,
                ShowImage = true
            };
        }

        private void RemoveImage()
        {
            var pr = (this.Data as ProjectRoot);
            pr.ModuleConfiguration.ModuleImage = null;
        }
    }
}