using System.IO;
using System.Xml.Serialization;
using FomodInfrastructure.Interface;

namespace MainApplication.Services
{
    public class Repository: IRepository
    {
        public string ProjectPath { get; }

        public Repository(string projectPath)
        {
            ProjectPath = projectPath;
        }

        private static bool SerializeObject<T>(T data, string path)
        {
            if (data == null || !File.Exists(path)) return false;
            using (var fs = File.Create(path))
            {
                var xmlSerializer = new XmlSerializer(typeof (T));
                xmlSerializer.Serialize(fs, data);
            }
            return true;
        }
        private static T DeserializeObject<T>(string path)
        {
            if (!File.Exists(path)) return default(T);
            using (var fs = File.OpenRead(path))
            {
                var xmlSerializer = new XmlSerializer(typeof (T));
                return (T)xmlSerializer.Deserialize(fs);
            }
        }
        public T LoadData<T>(string path)
        {
            throw new System.NotImplementedException();
        }

        public bool SaveData<T>(string path, T data)
        {
            throw new System.NotImplementedException();
        }

        public T GetData<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}