namespace FomodInfrastructure.Interface
{
    public interface IFileBrowserDialog
    {
        string SelectedPath { get; set; }
        string Filter { get; set; }
        bool ShowDialog();
        void Reset();
        bool Multiselect { get; set; }
        string[] SelectedPaths { get; }
        string StartFolder { get; set; }
    }
}