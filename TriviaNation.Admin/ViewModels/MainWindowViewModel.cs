using System;
using System.Windows;
using System.Windows.Controls;
using TriviaNation.Util;
using TriviaNation.Views;

namespace TriviaNation.ViewModels
{
	// Defines the data context of an AdminView
	public class MainWindowViewModel : ViewModel
	{

		public UserControl ContentViewBox
		{
			get;
			set;
		}


		private string _LoggedInUserName;

		public string LoggedInUserName
		{
			get { return _LoggedInUserName; }
			set
			{
				if (_LoggedInUserName != value)
				{
					_LoggedInUserName = value;
					OnPropertyChanged(nameof(LoggedInUserName));
				}
			}
		}

		public MainWindowViewModel()
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

		#region ManageQuestionBanks

		public void ExecuteManageQuestionBanksCommand(object ob)
		{
			//var newContentVM = new ModifyOrDeleteQuestionViewModel();
			var newContent = new ModifyOrDeleteQuestionView();

			ContentViewBox.Content = newContent;
		}

		private RelayCommand _ManageQuestionBanksCommand;

		public RelayCommand ManageQuestionBanksCommand
		{
			get { return _ManageQuestionBanksCommand ?? (_ManageQuestionBanksCommand = new RelayCommand(ExecuteManageQuestionBanksCommand)); }
			set { _ManageQuestionBanksCommand = value; }
		}

		#endregion

		#region ManageStudents

		public void ExecuteManageStudentsCommand(object ob)
		{
			var newContentVM = new StudentManagementViewModel();
			var newContent = new StudentManagementView(newContentVM);

			ContentViewBox.Content = newContent;
		}

		private RelayCommand _ManageStudentsCommand;

		public RelayCommand ManageStudentsCommand
		{
			get { return _ManageStudentsCommand ?? (_ManageStudentsCommand = new RelayCommand(ExecuteManageStudentsCommand)); }
			set { _ManageStudentsCommand = value; }
		}

		#endregion

		public EventHandler<object> TransitionChildView;

		#region ChangeDisplayedView

		#region DashboardCommand

		public void ExecuteDashboardCommand(object ob)
		{
			var newContentVM = new AdminDashboardViewModel();
			var newContent = new AdminDashboardView(newContentVM);

			ContentViewBox.Content = newContent;
		}

		private RelayCommand _DashboardCommand;

		public RelayCommand DashboardCommand
		{
			get { return _DashboardCommand ?? (_DashboardCommand = new RelayCommand(ExecuteDashboardCommand)); }
			set { _DashboardCommand = value; }
		}

		#endregion


		#endregion

		#region Logout

		public void ExecuteLogoutCommand(object ob)
		{
			App.OnLogout();

			DashboardCommand.Execute(null);

			LoggedInUserName = null;

			LoggedOut?.Invoke(this, null);
		}

		private RelayCommand _LougoutCommand;

		public RelayCommand LogoutCommand
		{
			get { return _LougoutCommand ?? (_LougoutCommand = new RelayCommand(ExecuteLogoutCommand)); }
			set { _LougoutCommand = value; }
		}


		public event EventHandler LoggedOut;

		#endregion
	}
}
