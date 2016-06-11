namespace FomodInfrastructure.Interfaces
{
    public interface IRepository<TData> where TData: IRepositoryData
    {
        RepositoryResult<TData> Create(string path);
        RepositoryResult<TData> Load(string path);
        RepositoryResult Save(string path);
        TData Data { get; }
    }
}