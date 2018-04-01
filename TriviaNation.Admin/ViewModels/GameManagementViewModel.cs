using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TriviaNation.Drivers;
using TriviaNation.Models;
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

		public GameManagementViewModel()
		{
			using (var db = new DynamoDBDriver())
			{
				_GameSessions = db.GetGameSessionsByInstructor(Application.Current.Properties["LoggedInUserId"].ToString());
			} 
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
