using System.ComponentModel;
using AspectInjector.Broker;
using System.Xml.Serialization;
using System;

namespace FomodInfrastructure.Aspect
{
    [Serializable]
    [AdviceInterfaceProxy(typeof (INotifyPropertyChanged))]
    public class AspectINotifyPropertyChanged : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        [Advice(InjectionPoints.After, InjectionTargets.Setter)]
        public void RaisePropertyChanged(
            [AdviceArgument(AdviceArgumentSource.Instance)] object targetInstance,
            [AdviceArgument(AdviceArgumentSource.TargetName)] string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(targetInstance, new PropertyChangedEventArgs(propertyName));
        }
    }
}