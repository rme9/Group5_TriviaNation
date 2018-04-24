using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TriviaNation.Core.Drivers;
using TriviaNation.Core.Models;
using TriviaNation.Util;

namespace TriviaNation.ViewModels
{
	public class StudentCreationViewModel : ViewModel
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		#region SaveCommand

		public bool CanExecuteSaveCommand(object ob)
		{
			return !string.IsNullOrEmpty(Name);
		}

		public void ExecuteSaveCommand(object ob)
		{
			var students = new List<StudentUser>();

			if (ob is ObservableCollection<object> list)
			{
				foreach (var item in list)
				{
					students.Add(item as StudentUser);
				}
			}

			var newStudent = new StudentUser(Name, Email)
			{
				Password = Password,
				InstructorId = Application.Current.Properties["LoggedInUserId"] as string
			};

			using (var db = new WebServiceDriver())
			{
				var didInsert = db.InsertUser(newStudent).Result;
			}

			CloseView?.Invoke(this, newStudent);
		}

		private RelayCommand _SaveCommand;

		public RelayCommand SaveCommand
		{
			get { return _SaveCommand ?? (_SaveCommand = new RelayCommand(ExecuteSaveCommand, CanExecuteSaveCommand)); }
			set { _SaveCommand = value; }
		}

		#endregion

		#region CancelCommand

		public void ExecuteCancelCommand(object ob)
		{
			CloseView?.Invoke(this, null);
		}

		private RelayCommand _CancelCommand;

		public RelayCommand CancelCommand
		{
			get { return _CancelCommand ?? (_CancelCommand = new RelayCommand(ExecuteCancelCommand)); }
			set { _CancelCommand = value; }
		}

		public event EventHandler<object> CloseView;

		#endregion
	}
}
