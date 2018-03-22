using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using TriviaNation.Models;

namespace TriviaNation.ViewModels
{
	// Defines the data context of a GameCreationView
	public class GameCreationViewModel : IViewModel
	{
		public string Name { get; set; }

		public IQuestionBank SelectedQuestionBank { get; set; }

		public ObservableCollection<IQuestionBank> AvailableQuestionBanks { get; set; }

		public ObservableCollection<IUser> AvailableStudents { get; set; }

		public GameCreationViewModel()
		{
		}

		#region SaveCommand

		public bool CanExecuteSaveCommand(object ob)
		{
			return !string.IsNullOrEmpty(Name);
		}

		public void ExecuteSaveCommand(object ob)
		{
			// TODO write newGame to database, call method to send emails to students.
			var students = new List<IUser>();

			if (ob is ObservableCollection<object> list)
			{
				foreach (var item in list)
				{
					students.Add(item as IUser);
				}
			}

			var newGame = new GameSession()
			{
				Name = Name,
				Students = students,
				QuestionBank = SelectedQuestionBank
			};

			CloseView?.Invoke(this, null);
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

		public event EventHandler CloseView;

		#endregion

	}
}
