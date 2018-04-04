using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TriviaNation.Core.Drivers;
using TriviaNation.Core.Models;
using TriviaNation.Util;

namespace TriviaNation.ViewModels
{
	public class AdminDashboardViewModel : ViewModel
	{
		#region Student List
		private List<StudentUser> _AllStudents;

		public List<StudentUser> AllStudents
		{
			get { return _AllStudents; }
			set
			{
				if (_AllStudents != value)
				{
					_AllStudents = value;
					OnPropertyChanged(nameof(AllStudents));
				}
			}
		}

		#endregion

		#region Question Banks

		private List<IQuestionBank> _AllQuestionBanks;
		public List<IQuestionBank> AllQuestionBanks
		{
			get { return _AllQuestionBanks;}
			set
			{
				if (_AllQuestionBanks != value)
				{
					_AllQuestionBanks = value;
					OnPropertyChanged(nameof(_AllQuestionBanks));
				}
			}
		}


		#endregion
		public AdminDashboardViewModel()
		{
			
		}

		public void UpdateViewAfterLogin()
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
