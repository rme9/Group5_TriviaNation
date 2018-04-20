using System;
using System.Windows.Input;

namespace TriviaNation.Util
{
	public class RelayCommand : ICommand
	{
		private readonly Action<object> _Execute;

		private readonly Predicate<object> _CanExecute;

		public RelayCommand(Action<object> execute, Predicate<object> canExecute)
		{
			_Execute = execute ?? throw new NullReferenceException();
			_CanExecute = canExecute;
		}

		public RelayCommand(Action<object> execute) : this(execute, null)
		{
			// This method is used when CanExecute is ALWAYS true.
			// It simply calls the other constructor with a null canExecute.
		}

		public event EventHandler CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value;
			remove => CommandManager.RequerySuggested -= value;
		}

		public bool CanExecute(object parameter)
		{
			return _CanExecute?.Invoke(parameter) ?? true;
		}

		public void Execute(object parameter)
		{
			_Execute.Invoke(parameter);
		}

	}
}
