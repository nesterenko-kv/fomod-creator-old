namespace FomodInfrastructure.Interfaces
{
    /// <summary>
    /// Модель данных репозитория
    /// </summary>
    public interface IData
    {
        /// <summary>
        ///   Путь к данным
        ///   </summary>
        string Source { get; }
    }
}