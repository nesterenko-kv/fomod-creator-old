namespace FomodInfrastructure.Interfaces
{
    public interface IRepository<TData> where TData: IData
    {
        RepositoryResult<TData> Create(string path);
        RepositoryResult<TData> Load(string path);
        RepositoryResult<TData> Save(string path);
        TData Data { get; }
    }
}