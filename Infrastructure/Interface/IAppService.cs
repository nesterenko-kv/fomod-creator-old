namespace FomodInfrastructure.Interface
{
    /// <summary>
    /// Обеспечивает инфраструктуру приложения
    /// </summary>
    public interface IAppService
    {
        void CloseApp();
        void InitilizeBaseModules();
    }
}