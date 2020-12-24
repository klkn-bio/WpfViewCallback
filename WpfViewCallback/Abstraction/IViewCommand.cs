using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfViewCallback.Abstraction
{
    /// <summary>
    /// Base ViewCommand
    /// </summary>
    public interface IViewCommand
    {
        /// <summary>
        /// How many view attached
        /// </summary>
        int ConnectedCount { get; }
    }

    /// <summary>
    /// Action without Parameters
    /// </summary>
    public interface IViewActionCommand : IViewCommand
    {
        /// <summary>
        /// Execute action on all attached Views
        /// </summary>
        void Execute();
    }

    /// <summary>
    /// Action with one parameter
    /// </summary>
    public interface IViewActionCommand<in TArg> : IViewCommand
    {
        /// <summary>
        /// Execute action on all attached Views
        /// </summary>
        void Execute(TArg arg1);
    }

    public interface IViewFuncCommand<out TResult> : IViewCommand
    {
        /// <summary>
        /// Execute function on all attached Views
        /// </summary>
        TResult[] Execute();
    }

    public interface IViewFuncCommand<in TArg, out TResult> : IViewCommand
    {
        /// <summary>
        /// Execute function on all attached Views
        /// </summary>
        TResult[] Execute(TArg arg1);
    }

}
