using System;
using AspectInjector.Broker;
using FomodInfrastructure.Aspects;
using FomodInfrastructure.Interfaces;

namespace FomodModel.Base
{
    [Aspect(typeof(AspectINotifyPropertyChanged)), Serializable]
    public class Project: IData
    {
        public Project(string source)
        {
            Source = source;
        }

        #region Properties

        public string Source { get; }
        
        public ModuleInformation ModuleInformation { get; set; }

        public ModuleConfiguration ModuleConfiguration { get; set; }

        #endregion

        public static Project Create(string source)
        {
            return new Project(source);
        }
    }
}