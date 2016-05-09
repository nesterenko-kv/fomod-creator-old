using System.Windows.Forms;
using FomodInfrastructure.Interface;


namespace MainApplication.Services
{
    public class FolderBrowserDialog : IFolderBrowserDialog
    {
        private readonly System.Windows.Forms.FolderBrowserDialog _dialog = new System.Windows.Forms.FolderBrowserDialog();

        #region IFolderBrowserDialog

        public string Description
        {
            get { return _dialog.Description; }
            set { _dialog.Description = value; }
        }

        public string SelectedPath
        {
            get { return _dialog.SelectedPath; }
            set { _dialog.SelectedPath = value; }
        }

        public bool ShowNewFolderButton
        {
            get { return _dialog.ShowNewFolderButton; }
            set { _dialog.ShowNewFolderButton = value; }
        }

        public void Reset()
        {
            _dialog.Reset();
        }

        public bool ShowDialog() => _dialog.ShowDialog() == DialogResult.OK;

        #endregion
    }
}