using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Welcome.Model
{
    [Serializable]
    public class ProjectLinkList
    {
        public ObservableCollection<ProjectLinkModel> Links { get; set; } = new ObservableCollection<ProjectLinkModel>();
    }
}
