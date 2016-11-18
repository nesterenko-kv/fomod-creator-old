namespace FOMOD.Creator.Interfaces
{
    public interface IMemoryService
    {
        bool IsMemorySizeChanged { get; }

        void GetMemorySize(object obj);

        void Reset(object obj);
    }
}
