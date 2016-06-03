using System;
using System.Xml.Serialization;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace FomodModel.AppModel
{
    [Aspect(typeof(AspectINotifyPropertyChanged)), Serializable]
    public class ProjectLinkModel
    {
        [XmlAttribute]
        public string FolderPath { get; set; }

        [XmlAttribute]
        public string ProjectName { get; set; }

        public static ProjectLinkModel Create(string projectName, string folderPath)
        {
            return new ProjectLinkModel { ProjectName = projectName, FolderPath = folderPath };
        }
    }
}