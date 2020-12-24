using System;
using System.Windows.Input;

namespace WpfViewCallback.Commands
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _action;

        public DelegateCommand(Action action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        #region Implementation of ICommand

        /// <inheritdoc />
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <inheritdoc />
        public void Execute(object parameter)
        {
            _action();
        }

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;

        #endregion
    }
}
