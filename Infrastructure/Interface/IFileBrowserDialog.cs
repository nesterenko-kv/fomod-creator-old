namespace FomodInfrastructure.Interface
{
    public interface IFileBrowserDialog
    {
        string SelectedPath { get; set; }
        string Filter { get; set; }
        bool ShowDialog();
        void Reset();
    }
}