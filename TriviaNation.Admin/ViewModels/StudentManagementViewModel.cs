using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DevExpress.Mvvm.POCO;
using TriviaNation.Core.Drivers;
using TriviaNation.Core.Models;
using TriviaNation.Util;
using TriviaNation.Views;

namespace TriviaNation.ViewModels
{
	public class StudentManagementViewModel : ViewModel
	{
		private List<StudentUser> _Students;

		public ObservableCollection<StudentUser> Students
		{
			get { return new ObservableCollection<StudentUser>(_Students ?? new List<StudentUser>()); }
			set { _Students = value.ToList(); }
		}


		public StudentUser SelectedStudent
		{
			get;
			set;
		}

		public StudentManagementViewModel()
		{
			TryLoadData();
		}

		private async void TryLoadData()
		{
			using (var db = new WebServiceDriver())
			{
				try
				{
					var currentUser = Application.Current.Properties["LoggedInUserId"] as string;
					_Students = await db.GetAllUsersByInstructor(currentUser);
					OnPropertyChanged(nameof(Students));
				}
				catch (Exception ex)
				{

				}
			}
		}

		#region NewStudentCommand

		public void ExecuteNewStudentCommand(object ob)
		{
			var newVM = new StudentCreationViewModel();

			var newStudentWindow = new StudentCreationView(newVM);

			newVM.CloseView += OnCloseStudentCreationWindow;

			newStudentWindow.ShowDialog();
		}

		private void OnCloseStudentCreationWindow(object sender, object e)
		{
			if (e is StudentUser ng)
			{
				_Students.Add(ng);
				OnPropertyChanged(nameof(Students));
			}
		}

		private RelayCommand _NewStudentCommand;

		public RelayCommand NewStudentCommand
		{
			get { return _NewStudentCommand ?? (_NewStudentCommand = new RelayCommand(ExecuteNewStudentCommand)); }
			set { _NewStudentCommand = value; }
		}

		#endregion

		#region DeleteStudentCommand

		public async void ExecuteDeleteStudentCommand(object ob)
		{
			if (SelectedStudent != null)
			{
				var itemDesc = "student user " + SelectedStudent.Name;

				var vm = new DeleteConfirmationViewModel(itemDesc);

				vm.CloseView += OnConfirmDeleteClosed;

				var confirmationPopup = new DeleteConfirmationVeiw(vm) {Owner = Application.Current.MainWindow};


				confirmationPopup.ShowDialog();
			}
		}

		private async void OnConfirmDeleteClosed(object sender, object e)
		{
			if (e is bool b && b)
			{
				using (var driver = new WebServiceDriver())
				{
					var didDelete = await driver.DeleteUser(SelectedStudent);
					if (didDelete)
					{
						_Students.Remove(SelectedStudent);
						OnPropertyChanged(nameof(Students));
					}
				}
			}
		}

		public bool CanExecuteDeleteStudentCommand(object ob)
		{
			return SelectedStudent != null;
		}

		private RelayCommand _DeleteStudentCommand;

		public RelayCommand DeleteStudentCommand
		{
			get { return _DeleteStudentCommand ?? (_DeleteStudentCommand = new RelayCommand(ExecuteDeleteStudentCommand, CanExecuteDeleteStudentCommand)); }
			set { _DeleteStudentCommand = value; }
		}

		#endregion
	}
}
