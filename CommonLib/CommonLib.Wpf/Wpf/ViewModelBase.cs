using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CommonLib.Wpf
{
    public abstract class ViewModelBase : BindableBase
    {
        private class DelegateCommand<T> : ICommand
        {
            Action<T> command;
            Func<T, bool> canExecute;
            string propName;

            public DelegateCommand(ViewModelBase viewModel, Action<T> command, Func<T, bool> canExecute = null, string propName = null)
            {
                this.command = command;
                this.canExecute = canExecute;
                this.propName = propName;

                if (canExecute != null && viewModel != null)
                {
                    viewModel.PropertyChanged += viewModel_PropertyChanged;
                }
            }

            public bool CanExecute(object parameter)
            {
                return canExecute?.Invoke((T)parameter) ?? true;
            }

            public void Execute(object parameter)
            {
                command?.Invoke((T)parameter);
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            private void viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (string.IsNullOrEmpty(propName) && e.PropertyName != propName)
                    return;

                Application.Current.Dispatcher.Invoke(() => CommandManager.InvalidateRequerySuggested());
            }
        }

        private class DelegateAsyncCommand<T> : ICommand
        {
            Func<T, Task> command;
            Func<T, bool> canExecute;
            string propName;
            bool isProcessing = false;

            public DelegateAsyncCommand(ViewModelBase viewModel, Func<T, Task> command, Func<T, bool> canExecute = null, string propName = null)
            {
                this.command = command;
                this.canExecute = canExecute;
                this.propName = propName;

                if (canExecute != null && viewModel != null)
                {
                    viewModel.PropertyChanged += viewModel_PropertyChanged;
                }
            }

            public bool CanExecute(object parameter)
            {
                return !isProcessing && (canExecute?.Invoke((T)parameter) ?? true);
            }

            public async void Execute(object parameter)
            {
                isProcessing = true;
                Application.Current.Dispatcher.Invoke(() => CommandManager.InvalidateRequerySuggested());
                await command?.Invoke((T)parameter);
                isProcessing = false;
                Application.Current.Dispatcher.Invoke(() => CommandManager.InvalidateRequerySuggested());
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            private void viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (string.IsNullOrEmpty(propName) && e.PropertyName != propName)
                    return;

                Application.Current.Dispatcher.Invoke(() => CommandManager.InvalidateRequerySuggested());
            }
        }

        protected void AddModelObserver<T>(T model) where T : INotifyPropertyChanged
        {
            model.PropertyChanged += (sender, e) => OnPropertyChanged(e.PropertyName);
        }

        /// <summary>
        /// ICommandの生成
        /// </summary>
        /// <param name="command"></param>
        /// <param name="canExecute"></param>
        /// <param name="propName">CanExecuteの変更にバインドされるプロパティ名。省略された場合全てのPropetyChangedと連動する</param>
        /// <returns></returns>
        protected ICommand CreateCommand<T>(Action<T> command, Func<T, bool> canExecute = null, string propName = null)
        {
            return new DelegateCommand<T>(this, command, canExecute, propName);
        }

        /// <summary>
        /// ICommandの生成
        /// </summary>
        /// <param name="command"></param>
        /// <param name="canExecute"></param>
        /// <param name="propName">CanExecuteの変更にバインドされるプロパティ名。省略された場合全てのPropetyChangedと連動する</param>
        /// <returns></returns>
        protected ICommand CreateCommand(Action command, Func<bool> canExecute = null, string propName = null)
        {
            return CreateCommand<object>(input => command(), canExecute == null ? null : input => canExecute(), propName);
        }

        /// <summary>
        /// ICommandの生成
        /// </summary>
        /// <param name="command"></param>
        /// <param name="canExecute"></param>
        /// <param name="propName">CanExecuteの変更にバインドされるプロパティ名。省略された場合全てのPropetyChangedと連動する</param>
        /// <returns></returns>
        protected ICommand CreateCommand<T>(Func<T, Task> command, Func<T, bool> canExecute = null, string propName = null)
        {
            return new DelegateAsyncCommand<T>(this, command, canExecute, propName);
        }

        /// <summary>
        /// ICommandの生成
        /// </summary>
        /// <param name="command"></param>
        /// <param name="canExecute"></param>
        /// <param name="propName">CanExecuteの変更にバインドされるプロパティ名。省略された場合全てのPropetyChangedと連動する</param>
        /// <returns></returns>
        protected ICommand CreateCommand(Func<Task> command, Func<bool> canExecute = null, string propName = null)
        {
            return CreateCommand<object>(input => command(), canExecute == null ? null : input => canExecute(), propName);
        }
    }
}
