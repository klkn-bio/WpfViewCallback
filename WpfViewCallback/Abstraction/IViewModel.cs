using System;
using System.Windows.Input;

namespace WpfViewCallback.Abstraction
{
    /// <summary>
    /// Theoretical VM
    /// </summary>
    public interface IViewModel
    {
        string Log { get; }

        #region Commands

        /// <summary>
        /// Do something
        /// </summary>
        ICommand ExecuteCommand { get; }

        /// <summary>
        /// CloseCommand
        /// </summary>
        ICommand CloseCommand { get; }

        #endregion Commands

        #region ViewCommands

        /// <summary>
        /// Action
        /// </summary>
        event Action Close;

        /// <summary>
        /// Action
        /// </summary>
        event Action ActionWithoutParameters;

        /// <summary>
        /// Action with Parameters
        /// </summary>
        event Action<string> ActionWithParameters;

        /// <summary>
        /// Function
        /// </summary>
        event Func<string> FunctionWithoutParameters;

        /// <summary>
        /// Function with Parameters
        /// </summary>
        event Func<string, string> FunctionWithParameters;

        #endregion ViewCommands

    }
}
