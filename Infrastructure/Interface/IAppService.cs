using System;

namespace FomodInfrastructure.Interface
{
    /// <summary>
    ///     Обеспечивает инфраструктуру приложения
    /// </summary>
    public interface IAppService
    {
        void CloseApp();

        string[] CommandLineArgs { get; }

        Version Version { get; }

        void CreateEditorModule<T>(IRepository<T> repository);

        /// <summary>
        /// Флаг для отслеживания, состояния загрузки проектов из командной строки. Основной целью флага служит отключение UI логики при закрузке проекта из командной строки
        /// </summary>
        bool IsOpenProjectsFromCommandLine { get; set; }
    }
}