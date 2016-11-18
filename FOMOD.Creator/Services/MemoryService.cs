namespace FOMOD.Creator.Services
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using FOMOD.Creator.Interfaces;

    public class MemoryService : IMemoryService
    {
        private long _lastMemorySize = -1;

        public bool IsMemorySizeChanged { get; private set; }

        public void GetMemorySize(object obj)
        {
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                var size = ms.Length;
                if (_lastMemorySize != -1 && _lastMemorySize != size)
                    IsMemorySizeChanged = true;
                _lastMemorySize = size;
            }
        }

        public void Reset(object obj)
        {
            GetMemorySize(obj);
            IsMemorySizeChanged = false;
        }
    }
}
