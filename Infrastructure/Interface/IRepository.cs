namespace FomodInfrastructure.Interface
{
    /// <summary>
    /// Интерфейс предназначенный для загрузки, сохранения и размещения в памяти данных 
    /// </summary>
    public interface IRepository<T>
    {
        T LoadData(string path = null);
        bool SaveData(string path = null);

        /// <summary>
        /// Получает ссылку на объект
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <returns>Возращет объект, если он был загружен в память</returns>
        T GetData();

    }
}
