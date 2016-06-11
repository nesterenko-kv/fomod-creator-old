using System.Windows.Forms;
using FomodInfrastructure.Interfaces;

namespace MainApplication.Services
{
    public class FileBrowserDialog : IFileBrowserDialog
    {
        #region Fields

        private readonly OpenFileDialog _dialog = new OpenFileDialog();

        #endregion

        #region IFileBrowserDialog

        public bool CheckFileExists
        {
            get { return _dialog.CheckFileExists; }
            set { _dialog.CheckFileExists = value; }
        }

        public string SelectedPath
        {
            get { return _dialog.FileName; }
            set { _dialog.FileName = value; }
        }

        public string StartFolder
        {
            get { return _dialog.InitialDirectory; }
            set { _dialog.InitialDirectory = value; }
        }

        public string[] SelectedPaths
        {
            get { return _dialog.FileNames; }
        }

        public string Filter
        {
            get { return _dialog.Filter; }
            set { _dialog.Filter = value; }
        }

        public void Reset()
        {
            _dialog.Reset();
        }

        public bool ShowDialog()
        {
            return _dialog.ShowDialog() == DialogResult.OK;
        }

        public bool Multiselect
        {
            get { return _dialog.Multiselect; }
            set { _dialog.Multiselect = value; }
        }

        #endregion
    }
}