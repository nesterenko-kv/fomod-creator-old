namespace FOMOD.Creator.Commands
{
    using System;
    using System.Reflection;

    public class WeakAction<T> : WeakAction, IExecuteWithObject
    {
        private Action<T> _staticAction;

        public WeakAction(Action<T> action)
            : this(action?.Target, action)
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

        public override string MethodName => _staticAction?.GetMethodInfo().Name ?? Method.Name;

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
            Execute((T) parameter);
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

        public WeakAction(Action action)
            : this(action?.Target, action)
        {
        }

        protected WeakAction()
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

        public virtual bool IsAlive => (_staticAction != null || Reference != null) && (_staticAction != null && Reference == null || Reference.IsAlive);

        public bool IsStatic => _staticAction != null;

        public virtual string MethodName => _staticAction?.GetMethodInfo().Name ?? Method.Name;

        public object Target => Reference?.Target;

        protected WeakReference ActionReference { get; set; }

        protected object ActionTarget => ActionReference?.Target;

        protected MethodInfo Method { get; set; }

        protected WeakReference Reference { get; set; }

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
