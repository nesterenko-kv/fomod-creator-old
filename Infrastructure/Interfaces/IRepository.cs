namespace FomodInfrastructure.Interfaces
{
    public interface IRepository<TData> where TData: IData
    {
        Result<TData> Create(string path);
        Result<TData> Load(string path);
        Result<TData> Save(string path);
        TData Data { get; }
    }
}