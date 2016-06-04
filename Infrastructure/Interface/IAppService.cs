using System;

namespace FomodInfrastructure.Interface
{
    /// <summary>
    ///     Обеспечивает инфраструктуру приложения
    /// </summary>
    public interface IAppService
    {
        void CloseApp();

        void InitilizeBaseModules();

        string[] CommandLineArgs { get; }

        Version Version { get; }

        void CreateEditorModule<T>(IRepository<T> repository);
    }
}