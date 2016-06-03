using System;
using System.Collections.ObjectModel;

namespace FomodModel.AppModel
{
    [Serializable]
    public class ProjectLinkList
    {
        public ObservableCollection<ProjectLinkModel> Links { get; } = new ObservableCollection<ProjectLinkModel>();
    }
}