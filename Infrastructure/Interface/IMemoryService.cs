namespace FomodInfrastructure.Interface
{
    public interface IMemoryService
    {
        bool IsMemorySizeChanged { get; }

        long GetMemorySize(object obj);

        void Reset(object obj);
    }
}