namespace FOMOD.Creator.ViewModels
{
    using System.Collections.Generic;
    using FOMOD.Creator.Domain.Models.ModuleCofiguration;
    using MahApps.Metro.Controls.Dialogs;

    public class PluginViewModel : FileWorkerBaseViewModel<Plugin>
    {
        public PluginViewModel(IDialogCoordinator dialogCoordinator)
            : base(dialogCoordinator)
        {
        }

        protected override void AddFile(List<string> paths)
        {
            TryAddFiles(Data.Files.Items, paths);
        }

        protected override void AddFolder(List<string> paths)
        {
            TryAddFolders(Data.Files.Items, paths);
        }

        protected override void AddImage()
        {
            if (TryGetImage(out string imagePath))
                Data.Image = Image.Create(imagePath);
        }

        protected override void BrowseImage()
        {
            if (TryGetImage(out string imagePath))
                Data.Image.Path = imagePath;
        }

        protected override void RemoveImage()
        {
            Data.Image = null;
        }
    }
}
