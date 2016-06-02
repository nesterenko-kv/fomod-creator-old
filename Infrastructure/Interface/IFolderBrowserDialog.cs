namespace FomodInfrastructure.Interface
{
    public interface IFolderBrowserDialog
    {
        string Description { get; set; }

        string SelectedPath { get; set; }

        bool ShowNewFolderButton { get; set; }

        bool CheckFolderExists { get; set; }

        void Reset();

        bool ShowDialog();
    }
}