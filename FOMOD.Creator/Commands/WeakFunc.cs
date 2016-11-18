namespace FOMOD.Creator.Commands
{
    using System;
    using System.Reflection;

    public class WeakFunc<T, TResult> : WeakFunc<TResult>, IExecuteWithObjectAndResult
    {
        private Func<T, TResult> _staticFunc;

        public WeakFunc(Func<T, TResult> func)
            : this(func?.Target, func)
        {
        }

        private WeakFunc(object target, Func<T, TResult> func)
        {
            if (func.GetMethodInfo().IsStatic)
            {
                _staticFunc = func;
                if (target == null)
                    return;
                Reference = new WeakReference(target);
            }
            else
            {
                Method = func.GetMethodInfo();
                FuncReference = new WeakReference(func.Target);
                Reference = new WeakReference(target);
            }
        }

        public override bool IsAlive
        {
            get
            {
                if (_staticFunc == null && Reference == null)
                    return false;
                if (_staticFunc == null)
                    return Reference.IsAlive;
                return Reference == null || Reference.IsAlive;
            }
        }

        public override string MethodName => _staticFunc?.GetMethodInfo().Name ?? Method.Name;

        public TResult Execute(T parameter = default(T))
        {
            if (_staticFunc != null)
                return _staticFunc(parameter);
            var funcTarget = FuncTarget;
            if (!IsAlive || Method == null || FuncReference == null || funcTarget == null)
                return default(TResult);
            return (TResult) Method.Invoke(funcTarget, new object[]
            {
                parameter
            });
        }

        public object ExecuteWithObject(object parameter)
        {
            return Execute((T) parameter);
        }

        public new void MarkForDeletion()
        {
            _staticFunc = null;
            base.MarkForDeletion();
        }
    }

    public class WeakFunc<TResult>
    {
        private Func<TResult> _staticFunc;

        public WeakFunc(Func<TResult> func)
            : this(func?.Target, func)
        {
        }

        protected WeakFunc()
        {
        }

        private WeakFunc(object target, Func<TResult> func)
        {
            if (func.GetMethodInfo().IsStatic)
            {
                _staticFunc = func;
                if (target == null)
                    return;
                Reference = new WeakReference(target);
            }
            else
            {
                Method = func.GetMethodInfo();
                FuncReference = new WeakReference(func.Target);
                Reference = new WeakReference(target);
            }
        }

        public virtual bool IsAlive
        {
            get
            {
                if (_staticFunc == null && Reference == null)
                    return false;
                if (_staticFunc != null && Reference == null)
                    return true;
                return Reference.IsAlive;
            }
        }

        public bool IsStatic => _staticFunc != null;

        public virtual string MethodName => _staticFunc?.GetMethodInfo().Name ?? Method.Name;

        public object Target => Reference?.Target;

        protected WeakReference FuncReference { get; set; }

        protected object FuncTarget => FuncReference?.Target;

        protected MethodInfo Method { get; set; }

        protected WeakReference Reference { get; set; }

        public TResult Execute()
        {
            if (_staticFunc != null)
                return _staticFunc();
            var funcTarget = FuncTarget;
            if (IsAlive && Method != null && FuncReference != null && funcTarget != null)
                return (TResult) Method.Invoke(funcTarget, null);
            return default(TResult);
        }

        protected void MarkForDeletion()
        {
            Reference = null;
            FuncReference = null;
            Method = null;
            _staticFunc = null;
        }
    }
}
