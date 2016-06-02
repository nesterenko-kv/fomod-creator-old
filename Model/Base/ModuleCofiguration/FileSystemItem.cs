using System;

namespace FomodModel.Base.ModuleCofiguration
{
    [Serializable]
    public class FileSystemItem : SystemItem
    {
        public static FileSystemItem Create()
        {
            return new FileSystemItem
            {
                Source = @"\ffga\kfdd.exe",
                Destination = @"\kfdd.exe"
            };
        }
        public static FileSystemItem Create(string Source, string Destination)
        {
            return new FileSystemItem
            {
                Source = Source,
                Destination = Destination
            };
        }
    }
}