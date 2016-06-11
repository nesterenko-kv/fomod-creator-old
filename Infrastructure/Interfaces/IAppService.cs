using System;
using System.Xml.Linq;

namespace FomodInfrastructure.Interfaces
{
    /// <summary>
    ///     Обеспечивает инфраструктуру приложения
    /// </summary>
    public interface IAppService
    {
        void CloseApp();

        string[] CommandLineArgs { get; }

        Version Version { get; }

        void CreateEditorModule<T>(IRepository<T> repository) where T : IRepositoryData;

        /// <summary>
        /// Флаг для отслеживания, состояния загрузки проектов из командной строки. Основной целью флага служит отключение UI логики при закрузке проекта из командной строки
        /// </summary>
        bool IsOpenProjectsFromCommandLine { get; set; }

        XElement GetXElementResource(string path);
    }
}