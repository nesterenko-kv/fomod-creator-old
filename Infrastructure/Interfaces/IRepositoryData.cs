namespace FomodInfrastructure.Interfaces
{
    /// <summary>
    /// Модель данных репозитория
    /// </summary>
    public interface IRepositoryData
    {
        /// <summary>
        ///   Путь к данным
        ///   </summary>
        string DataSource { get; }
    }
}