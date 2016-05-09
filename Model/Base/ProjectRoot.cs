using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;

namespace FomodModel.Base
{
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    public class ProjectRoot
    { 
        #region Properties

        public string FolderPath { get; set; }
        public ModuleInformation ModuleInformation { get; set; }
        public ModuleConfiguration ModuleConfiguration { get; set; }
        
        //public IList Items => new CompositeCollection()
        //{
        //    new CollectionContainer() {Collection = new[] {ModuleInformation}},
        //    new CollectionContainer() {Collection = new[] {ModuleConfiguration}}
        //};

        #endregion
    }
}