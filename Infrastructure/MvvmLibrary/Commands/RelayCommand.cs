using System;
using System.Reflection;
using System.Windows.Input;

namespace FomodInfrastructure.MvvmLibrary.Commands
{
    public sealed class RelayCommand : ICommand
    {
        private readonly WeakAction _execute;
        private readonly WeakFunc<bool> _canExecute;
        public event EventHandler CanExecuteChanged;
        public RelayCommand(Action execute) : this(execute, null)
        {

        }
        private RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            _execute = new WeakAction(execute);
            if (canExecute == null)
                return;
            _canExecute = new WeakFunc<bool>(canExecute);
        }

        public void RaiseCanExecuteChanged()
        {
            var eventHandler = CanExecuteChanged;
            eventHandler?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;
            if (_canExecute.IsStatic || _canExecute.IsAlive)
                return _canExecute.Execute();
            return false;
        }

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter) || _execute == null || !_execute.IsStatic && !_execute.IsAlive)
                return;
            _execute.Execute();
        }
    }
    public class RelayCommand<T> : ICommand
    {
        private readonly WeakAction<T> _execute;
        private readonly WeakFunc<T, bool> _canExecute;
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            _execute = new WeakAction<T>(execute);
            if (canExecute == null)
                return;
            _canExecute = new WeakFunc<T, bool>(canExecute);
        }

        public void RaiseCanExecuteChanged()
        {
            var eventHandler = CanExecuteChanged;
            eventHandler?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;
            if (!_canExecute.IsStatic && !_canExecute.IsAlive) return false;
            if (parameter == null && typeof(T).GetTypeInfo().IsValueType)
                return _canExecute.Execute();
            if (parameter == null || parameter is T)
                return _canExecute.Execute((T)parameter);
            return false;
        }

        public void Execute(object parameter)
        {
            var parameter1 = parameter;
            if (!CanExecute(parameter1) || _execute == null || !_execute.IsStatic && !_execute.IsAlive)
                return;
            if (parameter1 == null)
            {
                if (typeof(T).GetTypeInfo().IsValueType)
                    _execute.Execute();
                else
                    // ReSharper disable once ExpressionIsAlwaysNull
                    _execute.Execute((T)parameter1);
            }
            else
                _execute.Execute((T)parameter1);
        }
    }
}
