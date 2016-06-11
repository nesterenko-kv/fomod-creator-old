namespace FomodInfrastructure.Interfaces
{
    public interface IMemoryService
    {
        bool IsMemorySizeChanged { get; }

        long GetMemorySize(object obj);

        void Reset(object obj);
    }
}