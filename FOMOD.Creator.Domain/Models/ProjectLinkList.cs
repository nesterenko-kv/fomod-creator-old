namespace FOMOD.Creator.Domain.Models
{
    using System;
    using System.Collections.ObjectModel;
    using PropertyChanged;

    [Serializable]
    [ImplementPropertyChanged]
    public class ProjectLinkList
    {
        public ProjectLinkList()
        {
            Links = new ObservableCollection<ProjectLink>();
        }

        public ObservableCollection<ProjectLink> Links { get; }
    }
}
