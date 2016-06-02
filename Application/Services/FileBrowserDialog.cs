using System.Windows.Forms;
using FomodInfrastructure.Interface;

namespace MainApplication.Services
{
    public class FileBrowserDialog : IFileBrowserDialog
    {
        #region Fields

        private readonly OpenFileDialog _dialog = new OpenFileDialog();
        
        #endregion

        #region IFileBrowserDialog

        public string SelectedPath
        {
            get { return _dialog.FileName; }
            set { _dialog.FileName = value; }
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

        #endregion
    }
}