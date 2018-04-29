using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TriviaNation.Core.Drivers;
using TriviaNation.Core.Models;
using TriviaNation.UI.Views;
using TriviaNation.Util;

namespace TriviaNation.ViewModels
{
	public class GameManagementViewModel : ViewModel
	{
		public EventHandler CloseView;

		private List<IGameSession> _GameSessions;

		public ObservableCollection<IGameSession> GameSessions
		{
			get
			{
				return new ObservableCollection<IGameSession>(_GameSessions ?? new List<IGameSession>());
			}
			set { _GameSessions = value.ToList(); }
		}

		public ObservableCollection<int> StudentCount { get; set; }

		public GameManagementViewModel()
		{
			TryLoadData();

			if (_GameSessions == null)
			{
				_GameSessions = new List<IGameSession>();
			}

			var counts = new List<int>();
			foreach (var item in _GameSessions)
			{
				counts.Add(item.Students?.Count ?? 0);
			}

			StudentCount = new ObservableCollection<int>(counts);
		}

		public async void TryLoadData()
		{
			using (var db = new WebServiceDriver())
			{
				try
				{
					var user = Application.Current.Properties["LoggedInUserId"].ToString();
					_GameSessions = await db.GetGameSessionsByInstructor(user);
					
					OnPropertyChanged(nameof(GameSessions));
				}
				catch (Exception ex)
				{
					var m = ex.Message;
				}
			}
		}

		#region NewGameCommand

		public void ExecuteNewGameCommand(object ob)
		{
			var newVM = new GameCreationViewModel();

			var newGameWindow = new GameCreationView(newVM);

			newVM.CloseView += OnCloseGameCreationWindow;
			
			newGameWindow.ShowDialog();
		}

		private void OnCloseGameCreationWindow(object sender, object e)
		{
			if (e is GameSession ng)
			{
				_GameSessions.Add(ng);
				OnPropertyChanged(nameof(GameSessions));
			}
		}

		private RelayCommand _NewGameCommand;

		public RelayCommand NewGameCommand
		{
			get { return _NewGameCommand ?? (_NewGameCommand = new RelayCommand(ExecuteNewGameCommand)); }
			set { _NewGameCommand = value; }
		}


		#endregion
	}
}
