namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;

    [Serializable]
    public class FolderSystemItem : SystemItem
    {
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
