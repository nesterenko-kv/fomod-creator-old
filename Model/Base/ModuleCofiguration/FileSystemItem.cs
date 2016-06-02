using System;

namespace FomodModel.Base.ModuleCofiguration
{
    [Serializable]
    public class FileSystemItem : SystemItem
    {
        public static FileSystemItem Create(string source, string destination)
        {
            return new FileSystemItem { Source = source, Destination = destination };
        }
    }
}