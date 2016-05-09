using System.IO;
using System.Xml.Serialization;
using FomodInfrastructure.Interface;

namespace MainApplication.Services
{
    public class DataService : IDataService
    {
        public T DeserializeObject<T>(string path)
        {
            using (var fs = File.OpenRead(path))
            {
                var xmlSerializer = new XmlSerializer(typeof (T));
                return (T) xmlSerializer.Deserialize(fs);
            }
        }

        public T DeserializeObject<T>(Stream stream)
        {
            using (var s = stream)
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(s);
            }
        }

        public void SerializeObject<T>(T data, string path)
        {
            if (data == null) return;
            using (var fs = File.Create(path))
            {
                var xmlSerializer = new XmlSerializer(typeof (T));
                xmlSerializer.Serialize(fs, data);
            }
        }
    }
}