using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm.Native;
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
			get { return new ObservableCollection<IGameSession>(_GameSessions); }
			set { _GameSessions = value.ToList(); }
		}

		public ObservableCollection<int> StudentCount { get; set; }

		public GameManagementViewModel()
		{
			using (var db = new DynamoDBDriver())
			{
				_GameSessions = db.GetGameSessionsByInstructor(Application.Current.Properties["LoggedInUserId"].ToString());
			}

			var counts = new List<int>();
			foreach (var item in _GameSessions)
			{
				counts.Add(item.Students?.Count ?? 0);
			}

			StudentCount = new ObservableCollection<int>(counts);
		}

		#region NewGameCommand

		public void ExecuteNewGameCommand(object ob)
		{
			var newVM = new GameCreationViewModel();

			var newGameWindow = new GameCreationView(newVM);

			newVM.CloseView += OnCloseGameCreationWindow;
			
			newGameWindow.Show();
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
