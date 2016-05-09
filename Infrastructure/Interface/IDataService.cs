using System.IO;

namespace FomodInfrastructure.Interface
{
    public interface IDataService
    {
        void SerializeObject<T>(T data, string path);

        T DeserializeObject<T>(string path);
        T DeserializeObject<T>(Stream stream);
    }
}