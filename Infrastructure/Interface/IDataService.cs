using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FomodInfrastructure.Interface
{
    public interface IDataService
    {
        void SerializeObject<T>(T data, string path);
        T DeserializeObject<T>(string path);
    }
}
