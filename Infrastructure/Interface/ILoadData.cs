using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FomodInfrastructure.Interface
{
    /// <summary>
    /// Интерфейс предназначенный для загрузки, сохранения и размещения в памяти данных 
    /// </summary>
    public interface IRepository
    {
        T LoadData<T>(string path);
        bool SaveData<T>(string path, T Data);

        /// <summary>
        /// Получает ссылку на объект
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <returns>Возращет объект, если он был загружен в память</returns>
        T GetData<T>();
    }
}
