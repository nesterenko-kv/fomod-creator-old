using System;
using System.ComponentModel;
using AspectInjector.Broker;

namespace FomodInfrastructure.Aspect
{
    [Serializable, AdviceInterfaceProxy(typeof(INotifyPropertyChanged))]
    public class AspectINotifyPropertyChanged : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        [Advice(InjectionPoints.After, InjectionTargets.Setter)]
        public void RaisePropertyChanged([AdviceArgument(AdviceArgumentSource.Instance)] object targetInstance, [AdviceArgument(AdviceArgumentSource.TargetName)] string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(targetInstance, new PropertyChangedEventArgs(propertyName));
        }
    }
}