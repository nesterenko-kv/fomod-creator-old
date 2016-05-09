namespace FomodInfrastructure.Interface
{
    public interface IFileBrowserDialog
    {
        bool ShowDialog();
        void Reset();
        string SelectedPath { get; set; }
    }
}
