using FomodInfrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication.Services
{
    public class FileBrowserDialog: IFileBrowserDialog
    {
        private readonly System.Windows.Forms.OpenFileDialog _dialog = new System.Windows.Forms.OpenFileDialog();


        #region IFolderBrowserDialog

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
