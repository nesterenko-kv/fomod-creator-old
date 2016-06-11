using System.Diagnostics;
using AspectInjector.Broker;

namespace FomodInfrastructure.Aspects
{
    public class AcpectDebugLoger
    {
        [Advice(InjectionPoints.After, InjectionTargets.Setter)]
        public void AfterSetLog([AdviceArgument(AdviceArgumentSource.Instance)] object inst, [AdviceArgument(AdviceArgumentSource.TargetName)] string propertyName, [AdviceArgument(AdviceArgumentSource.TargetValue)] object value)
        {
            ////Debug.Print($"Set ");
            Debug.Print($"{inst.GetType().Name} Set {propertyName} = {value} [{value?.GetHashCode()}]");
        }

        [Advice(InjectionPoints.After, InjectionTargets.Getter)]
        public void AfterGetLog([AdviceArgument(AdviceArgumentSource.Instance)] object inst, [AdviceArgument(AdviceArgumentSource.TargetName)] string propertyName, [AdviceArgument(AdviceArgumentSource.TargetValue)] object value)
        {
            ////Debug.Print($"Get ");
            Debug.Print($"{inst.GetType().Name} Get {propertyName} = {value} [{value?.GetHashCode()}]");
        }
    }
}