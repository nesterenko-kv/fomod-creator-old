using System.IO;
using System.Windows.Forms;
using FomodInfrastructure.Interface;

namespace MainApplication.Services
{
    public class FolderBrowserDialog : IFolderBrowserDialog
    {
        #region Fields

        private readonly System.Windows.Forms.FolderBrowserDialog _dialog = new System.Windows.Forms.FolderBrowserDialog();

        #endregion

        #region IFolderBrowserDialog

        public bool CheckFolderExists { get; set; }

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
            CheckFolderExists = false;
        }

        public bool ShowDialog()
        {
            var successful = _dialog.ShowDialog() == DialogResult.OK;
            if (CheckFolderExists)
                return successful && Directory.Exists(_dialog.SelectedPath);
            return successful;
        }

        #endregion
    }
}