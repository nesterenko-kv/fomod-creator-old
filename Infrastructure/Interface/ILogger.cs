namespace FomodInfrastructure.Interface
{
    public interface ILogger
    {
        void Log(string msg);
        void LogCreate(object obj);
        void LogDisposable(object obj);
    }
}
