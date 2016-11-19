namespace FOMOD.Creator.Domain.Models
{
    using System;
    using PropertyChanged;

    [ImplementPropertyChanged]
    [Serializable]
    public class Project
    {
        public Project(string source)
        {
            Source = source;
        }

        public ModuleConfiguration Config { get; set; }

        public ModuleInformation Info { get; set; }

        public string Source { get; }
    }
}
