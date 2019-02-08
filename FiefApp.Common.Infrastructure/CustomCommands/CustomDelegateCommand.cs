using System;
using System.Windows.Input;

namespace FiefApp.Common.Infrastructure.CustomCommands
{
    public class CustomDelegateCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public CustomDelegateCommand(
            Action<object> execute,
            Func<object,
                bool> canExecute
            )
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(
            object parameter
            )
        {
            return _canExecute(parameter);
        }

        public void Execute(
            object parameter
            )
        {
            _execute(parameter);
        }
    }
}
