namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;

    [Serializable]
    public class FileSystemItem : SystemItem
    {
        public static FileSystemItem Create(string source, string destination)
        {
            return new FileSystemItem
            {
                Source = source,
                Destination = destination
            };
        }
    }
}
