using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using TriviaNation.Annotations;
using TriviaNation.Util;

namespace TriviaNation.ViewModels
{
	public class LoginPopupViewModel : ViewModel
	{
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

		public string UserType { get; set; }

		public LoginPopupViewModel()
		{

		}

		#region RadioButtons

		private ICommand _RadioButtonCommand;

		public ICommand RadioButtonCommand
		{
			get { return _RadioButtonCommand ?? (_RadioButtonCommand = new RelayCommand(ExecuteRadioButtonCommand)); }
			set { _RadioButtonCommand = value; }

		}

		private void ExecuteRadioButtonCommand(object param)
		{
			UserType = param as string;
		}

		#endregion

		#region LoginCommand

		public void ExecuteLoginCommand(object ob)
		{
			LoginMessage = null;

			var result = App.OnLogin(Email, null, UserType);

			if (result == null)
			{
				if (UserType.Equals("User"))
				{
					// TODO Load User View
				}
				else if (UserType.Equals("Admin"))
				{
					LoginComplete?.Invoke(this, new MainWindowViewModel());
				}
			}
			else
			{
				LoginMessage = result;
				Email = null;
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
