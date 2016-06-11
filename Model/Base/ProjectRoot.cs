using System;
using AspectInjector.Broker;
using FomodInfrastructure.Aspects;
using FomodInfrastructure.Interfaces;

namespace FomodModel.Base
{
    [Aspect(typeof(AspectINotifyPropertyChanged)), Serializable]
    public class ProjectRoot: IRepositoryData
    {
        #region Properties

        public string DataSource { get; set; }
        
        public ModuleInformation ModuleInformation { get; set; }

        public ModuleConfiguration ModuleConfiguration { get; set; }

        #endregion
    }
}