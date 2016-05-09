namespace FomodInfrastructure.Interface
{
    public interface IFileBrowserDialog
    {
        string SelectedPath { get; set; }
        bool ShowDialog();
        void Reset();
    }
}