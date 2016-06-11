using System.IO;
using System.Xml.Serialization;
using FomodInfrastructure.Interfaces;

namespace MainApplication.Services
{
    /// <summary>
    /// Вспомогательный класс загрузки и сохранения данных в/из xml
    /// </summary>
    public class DataService : IDataService
    {
        #region IDataService
        public TData LoadData<TData>(string path)
        {
            using (var fs = File.OpenRead(path))
            {
                var xmlSerializer = new XmlSerializer(typeof(TData));
                return (TData)xmlSerializer.Deserialize(fs);
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
        
        #endregion
    }
}