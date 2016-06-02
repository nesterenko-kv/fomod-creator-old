using System;

namespace FomodModel.Base.ModuleCofiguration
{
    [Serializable]
    public class FolderSystemItem : SystemItem
    {
        public static FolderSystemItem Create()
        {
            return new FolderSystemItem
            {
                Source = @"\ffga\folder\1\folderNew",
                Destination = @"\folderNew"
            };
        }

        public static SystemItem Create(string directory, string destination)
        {
            return new FolderSystemItem
            {
                Source = directory,
                Destination = destination
            };
        }
    }
}