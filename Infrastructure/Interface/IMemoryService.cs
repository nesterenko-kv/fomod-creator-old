namespace FomodInfrastructure.Interface
{
    public interface IMemoryService
    {
        long GetMemorySize(object obj);
        void Reset(object obj);
        long LastMemorySize { get; }
        bool IsMemorySizeChanged { get; }
    }
}
