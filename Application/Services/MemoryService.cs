using FomodInfrastructure.Interface;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MainApplication.Services
{
    public class MemoryService: IMemoryService
    {
        #region IMemoryService

        public long LastMemorySize { get; private set; } = -1;

        public bool IsMemorySizeChanged { get; private set; }

        public long GetMemorySize(object obj)
        {
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                var size = ms.Length;
                if (LastMemorySize != -1 && LastMemorySize != size)
                    IsMemorySizeChanged = true;
                return LastMemorySize = size;
            }
        }

        public void Reset(object obj)
        {
            GetMemorySize(obj);
            IsMemorySizeChanged = false;
        }
        
        #endregion
    }
}
