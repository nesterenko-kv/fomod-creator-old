using System;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace FomodModel.Base
{
    [Aspect(typeof(AspectINotifyPropertyChanged)), Serializable]
    public class ProjectRoot
    {
        #region Properties

        public string FolderPath { get; set; }

        public ModuleInformation ModuleInformation { get; set; }

        public ModuleConfiguration ModuleConfiguration { get; set; }

        #endregion
    }
}