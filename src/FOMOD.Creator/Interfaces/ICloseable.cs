namespace FOMOD.Creator.Interfaces
{
    public interface ICloseable
    {
        bool IsNeedSave { get; }
        void Close();
    }
}
