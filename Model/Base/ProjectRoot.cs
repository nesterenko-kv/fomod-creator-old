using System;
using AspectInjector.Broker;
using FomodInfrastructure.Aspects;
using FomodInfrastructure.Interfaces;

namespace FomodModel.Base
{
    [Aspect(typeof(AspectINotifyPropertyChanged)), Serializable]
    public class ProjectRoot: IData
    {
        public ProjectRoot(string dataSource)
        {
            DataSource = dataSource;
        }

        #region Properties

        public string DataSource { get; }
        
        public ModuleInformation ModuleInformation { get; set; }

        public ModuleConfiguration ModuleConfiguration { get; set; }

        #endregion

        public static ProjectRoot Create(string source)
        {
            return new ProjectRoot(source);
        }
    }
}