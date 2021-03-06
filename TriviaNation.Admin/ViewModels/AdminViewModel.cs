﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

		#region ManageGameSessions

		public void ExecuteManageGamesCommand(object ob)
		{
			TransitionChildView.Invoke(this, new GameManagementViewModel());
		}

		private RelayCommand _ManageGamesCommand;

		public RelayCommand ManageGamesCommand
		{
			get { return _ManageGamesCommand ?? (_ManageGamesCommand = new RelayCommand(ExecuteManageGamesCommand)); }
			set { _ManageGamesCommand = value; }
		}

		#endregion

		public EventHandler<object> TransitionChildView;

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
