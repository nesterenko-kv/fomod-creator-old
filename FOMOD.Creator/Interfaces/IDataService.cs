namespace FOMOD.Creator.Interfaces
{
    public interface IDataService
    {
        T LoadData<T>(string path);
        void SaveData<T>(T data, string path);
    }
}
