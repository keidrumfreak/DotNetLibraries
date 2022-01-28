using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CommonLib.Wpf
{
    public abstract class ViewModelBase : BindableBase
    {
        private class DelegateCommand : ICommand
        {
            Action<object> command;
            Func<object, bool> canExecute;
            string propName;

            public DelegateCommand(ViewModelBase viewModel, Action<object> command, Func<object, bool> canExecute = null, string propName = null)
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
                return canExecute?.Invoke(parameter) ?? true;
            }

            public void Execute(object parameter)
            {
                command?.Invoke(parameter);
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

        private class DelegateAsyncCommand : ICommand
        {
            Func<object, Task> command;
            Func<object, bool> canExecute;
            string propName;
            bool isProcessing = false;

            public DelegateAsyncCommand(ViewModelBase viewModel, Func<object, Task> command, Func<object, bool> canExecute = null, string propName = null)
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
                return !isProcessing && (canExecute?.Invoke(parameter) ?? true);
            }

            public async void Execute(object parameter)
            {
                isProcessing = true;
                Application.Current.Dispatcher.Invoke(() => CommandManager.InvalidateRequerySuggested());
                await command?.Invoke(parameter);
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
        protected ICommand CreateCommand(Action<object> command, Func<object, bool> canExecute = null, string propName = null)
        {
            return new DelegateCommand(this, command, canExecute, propName);
        }

        /// <summary>
        /// ICommandの生成
        /// </summary>
        /// <param name="command"></param>
        /// <param name="canExecute"></param>
        /// <param name="propName">CanExecuteの変更にバインドされるプロパティ名。省略された場合全てのPropetyChangedと連動する</param>
        /// <returns></returns>
        protected ICommand CreateCommand(Action command, Func<object, bool> canExecute = null, string propName = null)
        {
            return CreateCommand(input => command(), canExecute, propName);
        }

        /// <summary>
        /// ICommandの生成
        /// </summary>
        /// <param name="command"></param>
        /// <param name="canExecute"></param>
        /// <param name="propName">CanExecuteの変更にバインドされるプロパティ名。省略された場合全てのPropetyChangedと連動する</param>
        /// <returns></returns>
        protected ICommand CreateCommand(Func<object, Task> command, Func<object, bool> canExecute = null, string propName = null)
        {
            return new DelegateAsyncCommand(this, command, canExecute, propName);
        }

        /// <summary>
        /// ICommandの生成
        /// </summary>
        /// <param name="command"></param>
        /// <param name="canExecute"></param>
        /// <param name="propName">CanExecuteの変更にバインドされるプロパティ名。省略された場合全てのPropetyChangedと連動する</param>
        /// <returns></returns>
        protected ICommand CreateCommand(Func<Task> command, Func<object, bool> canExecute = null, string propName = null)
        {
            return CreateCommand(input => command(), canExecute, propName);
        }
    }
}
