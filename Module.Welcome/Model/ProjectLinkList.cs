using System;
using System.Collections.ObjectModel;

namespace Module.Welcome.Model
{
    [Serializable]
    public class ProjectLinkList
    {
        public ObservableCollection<ProjectLinkModel> Links { get; set; } = new ObservableCollection<ProjectLinkModel>();
    }
}
