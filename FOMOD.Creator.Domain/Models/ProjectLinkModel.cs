namespace FOMOD.Creator.Domain.Models
{
    using System;
    using System.Xml.Serialization;
    using PropertyChanged;

    [Serializable]
    [ImplementPropertyChanged]
    public class ProjectLink
    {
        public ProjectLink()
        {
        }

        public ProjectLink(string projectName, string folderPath)
            : this()
        {
            ProjectName = projectName;
            FolderPath = folderPath;
        }

        [XmlAttribute]
        public string FolderPath { get; set; }

        [XmlAttribute]
        public string ProjectName { get; set; }
    }
}
