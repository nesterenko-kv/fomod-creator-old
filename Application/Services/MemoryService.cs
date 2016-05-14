using FomodInfrastructure.Interface;
using FomodModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication.Services
{
    public class MemoryService: IMemoryService
    {
        IDataService _dataService;

        public long LastMemorySize { get; private set; } = -1;
        public bool IsMemorySizeChanged { get; private set; }


        public MemoryService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public long GetMemorySize(object obj)
        {
            byte[] Array;
            long size = 0;

            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                Array = ms.ToArray();
            }
            size = Array.Length;

            if (LastMemorySize != -1 && LastMemorySize != size) IsMemorySizeChanged = true;
            return LastMemorySize = size;

        }

        public void Reset(object obj)
        {
            GetMemorySize(obj);
            IsMemorySizeChanged = false;
        }
    }
}
