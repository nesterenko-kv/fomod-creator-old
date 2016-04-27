using AspectInjector.Broker;
using System.ComponentModel;

namespace FomodInfrastructure.Aspect
{
    [AdviceInterfaceProxy(typeof(INotifyPropertyChanged))]
    public class AspectINotifyPropertyChanged : INotifyPropertyChanged
    {
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
