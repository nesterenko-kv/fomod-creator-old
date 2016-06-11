namespace FomodInfrastructure.Interfaces
{
    public interface IFileBrowserDialog
    {
        bool CheckFileExists { get; set; }

        string SelectedPath { get; set; }

        string Filter { get; set; }

        bool Multiselect { get; set; }

        string[] SelectedPaths { get; }

        string StartFolder { get; set; }

        bool ShowDialog();

        void Reset();
    }
}