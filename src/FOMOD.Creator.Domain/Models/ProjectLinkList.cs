namespace FOMOD.Creator.Domain.Models
{
    using System;
    using System.Collections.ObjectModel;
    using PropertyChanged;

    [Serializable]
    [ImplementPropertyChanged]
    public class ProjectLinkList
    {
        public ObservableCollection<ProjectLink> Links { get; } = new ObservableCollection<ProjectLink>();
    }
}
