using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TriviaNation.Models;
using TriviaNation.Util;

namespace TriviaNation.ViewModels
{
	// Defines the data context of an AdminView
	public class AdminViewModel : IViewModel
	{
		public string LoggedInUserName
		{
			get { return Application.Current.Properties["LoggedInUserName"] as string; }
			set { }
		}

		public AdminViewModel()
		{
			
		}

		#region Logout

		public void ExecuteLogoutCommand(object ob)
		{
			App.OnLogout();
		}

		private RelayCommand _LougoutCommand;

		public RelayCommand LogoutCommand
		{
			get { return _LougoutCommand ?? (_LougoutCommand = new RelayCommand(ExecuteLogoutCommand)); }
			set { _LougoutCommand = value; }
		}


		#endregion
	}
}
