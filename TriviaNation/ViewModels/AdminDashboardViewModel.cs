using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TriviaNation.Drivers;
using TriviaNation.Models;
using TriviaNation.Util;

namespace TriviaNation.ViewModels
{
	public class AdminDashboardViewModel
	{
		public List<StudentUser> AllStudents { get; set; }

		public List<IQuestionBank> AllQuestionBanks { get; set; }

		public AdminDashboardViewModel()
		{
			using (var db = new DynamoDBDriver())
			{
				var currentUser = Application.Current.Properties["LoggedInUserId"] as string;
				AllStudents = db.GetAllUsersByInstructor(currentUser);
				AllQuestionBanks = db.GetQuestionBanksByInstructor(currentUser);
			}
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
