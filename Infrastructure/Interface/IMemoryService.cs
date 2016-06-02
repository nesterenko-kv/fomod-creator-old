namespace FomodInfrastructure.Interface
{
    public interface IMemoryService
    {
        long GetMemorySize(object obj);
        void Reset(object obj);
        bool IsMemorySizeChanged { get; }
    }
}
