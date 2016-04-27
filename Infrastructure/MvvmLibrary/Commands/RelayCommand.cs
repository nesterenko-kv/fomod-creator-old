using System;
using System.Windows.Input;


namespace FomodInfrastructure.MvvmLibrary
{


    public class RelayCommand : ICommand
    {
        public bool IsNullCommand { get; set; }

        private Action _execute_with_param;
        private Action<object> _execute;
        private Predicate<object> _canExecute;

        public Action Execute_with_paramProperty { get { return _execute_with_param; } set { _execute_with_param = value; } }
        public Action<object> ExecuteProperty { get { return _execute; } set { _execute = value; } }
        public Predicate<object> CanExecuteProperty { get { return _canExecute; } set { _canExecute = value; } }

        public RelayCommand() { _execute = (p) => { }; IsNullCommand = true; }
        public RelayCommand(Action<object> execute) : this(execute, null) { }
        public RelayCommand(Action execute) : this(execute, null) { }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _execute_with_param = null;
            _canExecute = canExecute;

            IsNullCommand = false;
        }
        public RelayCommand(Action execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = null;
            _execute_with_param = execute;
            _canExecute = canExecute;

            IsNullCommand = false;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return _canExecute == null ? true : _canExecute(parameter); }
        public void Execute(object parameter)
        {
            if (_execute != null)
            {

                _execute.Invoke(parameter);
            }
            else
            {
                if (_execute_with_param != null)
                {
                    _execute_with_param.Invoke();
                }
            }
        }
    }


   
}
