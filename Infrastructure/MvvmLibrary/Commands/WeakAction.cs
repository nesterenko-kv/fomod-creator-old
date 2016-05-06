using System;
using System.Reflection;

namespace FomodInfrastructure.MvvmLibrary.Commands
{
    public class WeakAction<T> : WeakAction, IExecuteWithObject
    {
        private Action<T> _staticAction;
        public override string MethodName => _staticAction?.GetMethodInfo().Name ?? Method.Name;

        public override bool IsAlive
        {
            get
            {
                if (_staticAction == null && Reference == null)
                    return false;
                if (_staticAction == null)
                    return Reference.IsAlive;
                return Reference == null || Reference.IsAlive;
            }
        }

        public WeakAction(Action<T> action): this(action?.Target, action)
        {

        }
        private WeakAction(object target, Action<T> action)
        {
            if (action.GetMethodInfo().IsStatic)
            {
                _staticAction = action;
                if (target == null)
                    return;
                Reference = new WeakReference(target);
            }
            else
            {
                Method = action.GetMethodInfo();
                ActionReference = new WeakReference(action.Target);
                Reference = new WeakReference(target);
            }
        }

        public void Execute(T parameter = default(T))
        {
            if (_staticAction != null)
                _staticAction(parameter);
            else
            {
                var actionTarget = ActionTarget;
                if (IsAlive && Method != null && ActionReference != null && actionTarget != null)
                    Method.Invoke(actionTarget, new object[]
                    {
                        parameter
                    });
            }
        }

        public void ExecuteWithObject(object parameter)
        {
            Execute((T)parameter);
        }
        
        public new void MarkForDeletion()
        {
            _staticAction = null;
            base.MarkForDeletion();
        }
    }

    public class WeakAction
    {
        private Action _staticAction;
        protected MethodInfo Method { get; set; }
        public virtual string MethodName => _staticAction?.GetMethodInfo().Name ?? Method.Name;
        protected WeakReference ActionReference { get; set; }
        protected WeakReference Reference { get; set; }
        public bool IsStatic => _staticAction != null;
        public virtual bool IsAlive => (_staticAction != null || Reference != null) && (_staticAction != null && Reference == null || Reference.IsAlive);
        public object Target => Reference?.Target;
        protected object ActionTarget => ActionReference?.Target;
        protected WeakAction()
        {

        }
        public WeakAction(Action action): this(action?.Target, action)
        {

        }
        private WeakAction(object target, Action action)
        {
            if (action.GetMethodInfo().IsStatic)
            {
                _staticAction = action;
                if (target == null)
                    return;
                Reference = new WeakReference(target);
            }
            else
            {
                Method = action.GetMethodInfo();
                ActionReference = new WeakReference(action.Target);
                Reference = new WeakReference(target);
            }
        }
        public void Execute()
        {
            if (_staticAction != null)
                _staticAction();
            else
            {
                var actionTarget = ActionTarget;
                if (!IsAlive || Method == null || ActionReference == null || actionTarget == null)
                    return;
                Method.Invoke(actionTarget, null);
            }
        }
        protected void MarkForDeletion()
        {
            Reference = null;
            ActionReference = null;
            Method = null;
            _staticAction = null;
        }
    }
}