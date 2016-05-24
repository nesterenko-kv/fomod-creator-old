using System.Windows.Forms;
using FomodInfrastructure.Interface;

namespace MainApplication.Services
{
    public class FileBrowserDialog : IFileBrowserDialog
    {
        private readonly OpenFileDialog _dialog = new OpenFileDialog();

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

        public bool ShowDialog() => _dialog.ShowDialog() == DialogResult.OK;

        #endregion
    }
}