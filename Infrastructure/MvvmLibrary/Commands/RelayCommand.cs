using System;
using System.Windows.Input;

namespace FomodInfrastructure.MvvmLibrary.Commands
{


    public class RelayCommand : ICommand
    {
        public bool IsNullCommand { get; set; }
        public Action ExecuteWithParamProperty { get; set; }
        public Action<object> ExecuteProperty { get; set; }
        public Predicate<object> CanExecuteProperty { get; set; }
        public RelayCommand() { ExecuteProperty = p => { }; IsNullCommand = true; }
        public RelayCommand(Action<object> execute) : this(execute, null) { }
        public RelayCommand(Action execute) : this(execute, null) { }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            ExecuteProperty = execute;
            ExecuteWithParamProperty = null;
            CanExecuteProperty = canExecute;

            IsNullCommand = false;
        }
        public RelayCommand(Action execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            ExecuteProperty = null;
            ExecuteWithParamProperty = execute;
            CanExecuteProperty = canExecute;

            IsNullCommand = false;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => CanExecuteProperty == null || CanExecuteProperty(parameter);

        public void Execute(object parameter)
        {
            if (ExecuteProperty != null)
                ExecuteProperty.Invoke(parameter);
            else
                ExecuteWithParamProperty?.Invoke();
        }
    }


   
}
