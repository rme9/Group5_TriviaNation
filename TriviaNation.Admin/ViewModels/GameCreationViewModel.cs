﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TriviaNation.Core.Drivers;
using TriviaNation.Core.Models;
using TriviaNation.Util;

namespace TriviaNation.ViewModels
{
	// Defines the data context of a GameCreationView
	public class GameCreationViewModel : ViewModel
	{
		public string Name { get; set; }

		public QuestionBank SelectedQuestionBank { get; set; }

		private List<IQuestionBank> _AvailableQuestionBanks;

		public ObservableCollection<IQuestionBank> AvailableQuestionBanks
		{
			get{ return new ObservableCollection<IQuestionBank>(_AvailableQuestionBanks ?? new List<IQuestionBank>()); }
			set { _AvailableQuestionBanks = value.ToList(); }
		}

		private List<StudentUser> _AvailableStudents;

		public ObservableCollection<StudentUser> AvailableStudents
		{
			get { return new ObservableCollection<StudentUser>(_AvailableStudents ?? new List<StudentUser>()); }
			set
			{	
				_AvailableStudents = value.ToList();
			}
		}
		
		public GameCreationViewModel()
		{
			TryLoadData();
		}

		#region Database Query

		public async void TryLoadData()
		{
			var user = Application.Current.Properties["LoggedInUserId"] as string;

			using (var db = new WebServiceDriver())
			{
				_AvailableStudents = await db.GetAllUsersByInstructor(user);
				OnPropertyChanged(nameof(AvailableStudents));

				_AvailableQuestionBanks = await db.GetQuestionBanksByInstructor(user);
				OnPropertyChanged(nameof(AvailableQuestionBanks));
			}
		}

		#endregion

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

			var newGame = new GameSession()
			{
				Name = Name,
				Students = students,
				QuestionBank = SelectedQuestionBank
			};

			using (var db = new WebServiceDriver())
			{
				var didInsert = db.InsertGameSession(newGame, Application.Current.Properties["LoggedInUserId"] as string).Result;
			}

			CloseView?.Invoke(this, newGame);
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
