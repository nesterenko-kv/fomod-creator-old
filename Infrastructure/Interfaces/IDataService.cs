namespace FomodInfrastructure.Interfaces
{
    public interface IDataService
    {
        void SaveData<TData>(TData data, string path);

        TData LoadData<TData>(string path);
    }
}