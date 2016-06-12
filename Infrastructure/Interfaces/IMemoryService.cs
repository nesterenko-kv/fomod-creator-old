namespace FomodInfrastructure.Interfaces
{
    public interface IMemoryService
    {
        bool IsMemorySizeChanged { get; }

        void GetMemorySize(object obj);

        void Reset(object obj);
    }
}