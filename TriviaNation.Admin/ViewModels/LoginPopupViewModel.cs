﻿using System;
using System.Windows.Input;
using TriviaNation.Annotations;
using TriviaNation.Core.Models;
using TriviaNation.Util;

namespace TriviaNation.ViewModels
{
	public class LoginPopupViewModel : ViewModel
	{
		#region Login Message
		private string _LoginMessage;

		public string LoginMessage
		{
			get { return _LoginMessage; }
			set
			{
				if (_LoginMessage != value)
				{
					_LoginMessage = value;
					OnPropertyChanged(nameof(LoginMessage));
				}
			}
		}

		#endregion

		#region Email
		private string _Email;

		public string Email
		{
			get { return _Email; }
			set
			{
				if (_Email != value)
				{
					_Email = value;
					OnPropertyChanged(nameof(Email));
				}
			}
		}

		#endregion

		#region Password

		private string _Password;

		public string Password
		{
			get { return _Password; }
			set
			{
				if (_Password != value)
				{
					_Password = value;
					OnPropertyChanged(nameof(Password));
				}
			}
		}

		#endregion

		#region LoginCommand

		public async void ExecuteLoginCommand(object ob)
		{
			try
			{
				LoginMessage = null;

				await App.OnLogin(Email, Password);

				var result = App.LoginMessage;

				if (result == null)
				{
					LoginComplete?.Invoke(this, new MainWindowViewModel());
				}
				else
				{
					LoginMessage = result;
					Password = null;
				}
			}
			catch (Exception ex)
			{
				LoginMessage = "Unable to Log In";
			}
		}

		public bool CanExecuteLoginCommand(object ob)
		{
			return Email != null;
		}


		private ICommand _LoginCommand;

		public ICommand LoginCommand
		{
			get { return _LoginCommand ?? (_LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand)); }
			set { _LoginCommand = value; }
		}

		public event EventHandler<Object> LoginComplete;

		#endregion

	}
}
