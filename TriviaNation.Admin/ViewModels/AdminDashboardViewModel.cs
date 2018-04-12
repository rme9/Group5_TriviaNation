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

		public List<IQuestionBank> AllQuestionBanks { get; set; }

		public AdminDashboardViewModel()
		{
			TryLoadData();
		}

		private async void TryLoadData()
		{
			using (var serv = new WebServiceDriver())
			{
				var currentUser = Application.Current.Properties["LoggedInUserId"] as string;
				AllStudents = await serv.GetAllUsersByInstructor(currentUser);
				//AllQuestionBanks = db.GetQuestionBanksByInstructor(currentUser);
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
