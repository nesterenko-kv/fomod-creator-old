using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using FomodInfrastructure.Interface;

namespace MainApplication.Services
{
    public class MemoryService : IMemoryService
    {
        #region Fields

        private long _lastMemorySize = -1;

        #endregion

        #region IMemoryService

        public bool IsMemorySizeChanged { get; private set; }

        public long GetMemorySize(object obj)
        {
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                var size = ms.Length;
                if (_lastMemorySize != -1 && _lastMemorySize != size)
                    IsMemorySizeChanged = true;
                return _lastMemorySize = size;
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