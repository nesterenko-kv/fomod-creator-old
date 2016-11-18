namespace FOMOD.Creator.Services
{
    using System.IO;
    using System.Xml.Serialization;
    using FOMOD.Creator.Interfaces;

    public class DataService : IDataService
    {
        public TData LoadData<TData>(string path)
        {
            using (var fs = File.OpenRead(path))
            {
                var xmlSerializer = new XmlSerializer(typeof(TData));
                return (TData) xmlSerializer.Deserialize(fs);
            }
        }

        public void SaveData<TData>(TData data, string path)
        {
            if (data == null)
                return;
            using (var fs = File.Create(path))
            {
                var xmlSerializer = new XmlSerializer(typeof(TData));
                xmlSerializer.Serialize(fs, data);
            }
        }
    }
}
