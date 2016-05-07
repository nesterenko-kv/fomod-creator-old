using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FomodInfrastructure.Interface
{
    public interface IFileBrowserDialog
    {
        bool ShowDialog();
        void Reset();
        string SelectedPath { get; set; }
    }
}
