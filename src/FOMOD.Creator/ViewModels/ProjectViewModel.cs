namespace FOMOD.Creator.ViewModels
{
    using System.Collections.Generic;
    using FOMOD.Creator.Domain.Models;
    using FOMOD.Creator.Domain.Models.ModuleCofiguration;
    using MahApps.Metro.Controls.Dialogs;

    public class ProjectViewModel : FileWorkerBaseViewModel<Project>
    {
        public ProjectViewModel(IDialogCoordinator dialogCoordinator)
            : base(dialogCoordinator)
        {
        }

        protected override void AddFile(List<string> paths)
        {
            TryAddFiles(Data.Config.RequiredInstallFiles.Items, paths);
        }

        protected override void AddFolder(List<string> paths)
        {
            TryAddFolders(Data.Config.RequiredInstallFiles.Items, paths);
        }

        protected override void AddImage()
        {
            if (TryGetImage(out string imagePath))
                Data.Config.ModuleImage = HeaderImage.Create(imagePath);
        }

        protected override void BrowseImage()
        {
            if (TryGetImage(out string imagePath))
                Data.Config.ModuleImage.Path = imagePath;
        }

        protected override void RemoveImage()
        {
            Data.Config.ModuleImage = null;
        }
    }
}
