using System;
using System.Collections.Generic;
using System.Windows;
using TriviaNation.Core.Drivers;
using TriviaNation.Core.Models;
using TriviaNation.Util;

namespace TriviaNation.ViewModels
{
	public class AdminDashboardViewModel : ViewModel
	{
		public List<StudentUser> _AllStudents;

		public List<StudentUser> AllStudents
		{
			get { return _AllStudents; }
			set
			{
				if (value != _AllStudents)
				{
					_AllStudents = value;
					OnPropertyChanged(nameof(AllStudents));
				}
			}
		}

		public List<IQuestionBank> _AllQuestionBanks;

		public List<IQuestionBank> AllQuestionBanks
		{
			get { return _AllQuestionBanks; }
			set
			{
				if (_AllQuestionBanks != value)
				{
					_AllQuestionBanks = value;
					OnPropertyChanged(nameof(AllQuestionBanks));
				}
			}
		}

		public AdminDashboardViewModel()
		{
			TryLoadData();
		}

		private async void TryLoadData()
		{
			try
			{
				using (var serv = new WebServiceDriver())
				{
					var currentUser = Application.Current.Properties["LoggedInUserId"] as string;
					AllStudents = await serv.GetAllUsersByInstructor(currentUser);
					AllQuestionBanks = await serv.GetQuestionBanksByInstructor(currentUser);
				}
			}
			catch (Exception ex)
			{
				var m = ex.Message;
			}
		}

		public void UpdateViewAfterLogin()
		{
			TryLoadData();
		}

		#region ManageGameSessions

		public void ExecuteManageGamesCommand(object ob)
		{

		}

		private RelayCommand _ManageGamesCommand;

		public RelayCommand ManageGamesCommand
		{
			get { return _ManageGamesCommand ?? (_ManageGamesCommand = new RelayCommand(ExecuteManageGamesCommand)); }
			set { _ManageGamesCommand = value; }
		}

		#endregion
	}
}
