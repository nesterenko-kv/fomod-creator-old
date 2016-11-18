namespace FOMOD.Creator.Interfaces
{
    using FOMOD.Creator.Domain.Models;

    public interface IProjectManager
    {
        Project Data { get; }
        Result<Project> Create(string path);
        Result<Project> Load(string path);
        void Save(string path);
    }
}
