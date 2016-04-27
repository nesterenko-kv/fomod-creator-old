using AspectInjector.Broker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FomodInfrastructure.Aspect
{
    [AdviceInterfaceProxy(typeof(INotifyPropertyChanged))]
    public class Aspect_INotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [Advice(InjectionPoints.After, InjectionTargets.Setter)]
        public void RaisePropertyChanged(
            [AdviceArgument(AdviceArgumentSource.Instance)] object targetInstance,
            [AdviceArgument(AdviceArgumentSource.TargetName)] string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(targetInstance, new PropertyChangedEventArgs(propertyName));
            }
        }
    }



}
