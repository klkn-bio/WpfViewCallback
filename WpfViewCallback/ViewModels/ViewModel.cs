using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using WpfViewCallback.Abstraction;
using WpfViewCallback.Commands;

namespace WpfViewCallback.ViewModels
{
    public class ViewModel : IViewModel, INotifyPropertyChanged
    {
        public ViewModel()
        {
            ExecuteCommand = new DelegateCommand(Execute);
            CloseCommand = new DelegateCommand(CloseWindow);
        }

        private void CloseWindow()
        {
            Close?.Invoke();
        }

        private void Execute()
        {
            Log = null;
            var log = new StringBuilder();
            log.AppendLine($"Executing {nameof(ActionWithoutParameters)}");
            Log = log.ToString();
            ActionWithoutParameters?.Invoke();

            log.AppendLine();
            log.AppendLine($"Executing {nameof(ActionWithParameters)}");
            Log = log.ToString();
            ActionWithParameters?.Invoke("Hello Action with Parameters.");

            log.AppendLine();
            log.AppendLine($"Executing {nameof(FunctionWithoutParameters)}");
            var results = FunctionWithoutParameters
                ?.GetInvocationList()
                ?.Select(f => f.DynamicInvoke() as string)
                ?.ToArray()
                ?? Array.Empty<string>();
            log.AppendLine($"Receive {results.Length} answers:");
            foreach (var result in results)
            {
                log.AppendLine($"\t{result}");
            }

            log.AppendLine();
            log.AppendLine($"Executing {nameof(FunctionWithParameters)}");
            results = FunctionWithParameters
                              ?.GetInvocationList()
                              ?.Select(f => f.DynamicInvoke("Hello FunctionWithParameters.") as string)
                              ?.ToArray()
                          ?? Array.Empty<string>();
            log.AppendLine($"Receive {results.Length} answers:");
            foreach (var result in results)
            {
                log.AppendLine($"\t{result}");
            }
            Log = log.ToString();
        }


        #region Implementation of IViewModel

        public string Log
        {
            get => _log;
            private set
            {
                if (_log == value)
                    return;
                _log = value;
                OnPropertyChanged();
            }
        }

        private string _log;

        /// <inheritdoc />
        public ICommand ExecuteCommand { get; }

        /// <inheritdoc />
        public ICommand CloseCommand { get; }

        /// <inheritdoc />
        public event Action Close;

        /// <inheritdoc />
        public event Action ActionWithoutParameters;

        /// <inheritdoc />
        public event Action<string> ActionWithParameters;

        /// <inheritdoc />
        public event Func<string> FunctionWithoutParameters;

        /// <inheritdoc />
        public event Func<string, string> FunctionWithParameters;

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
