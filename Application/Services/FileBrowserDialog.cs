using FomodInfrastructure.Interface;

namespace MainApplication.Services
{
    public class FileBrowserDialog: IFileBrowserDialog
    {
        private readonly System.Windows.Forms.OpenFileDialog _dialog = new System.Windows.Forms.OpenFileDialog();

        #region IFileBrowserDialog

        public string SelectedPath
        {
            get { return _dialog.FileName; }
            set { _dialog.FileName = value; }
        }
        
        public void Reset()
        {
            _dialog.Reset();
        }

        public bool ShowDialog() => _dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK;

        #endregion
    }
}
