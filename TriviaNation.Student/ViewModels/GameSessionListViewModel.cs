using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaNation.Core.Drivers;
using TriviaNation.Core.Models;
using TriviaNation.Util;
using TriviaNation.ViewModels;
using Application = System.Windows.Application;


namespace TriviaNation.Student.ViewModels
{
    public class GameSessionListViewModel : ViewModel
    {
        public StudentUser Student;

        public ObservableCollection<GameSession> Sessions
        {
            get
            {
                return new ObservableCollection<GameSession>(_Sessions ?? new List<GameSession>());
            }
            set { _Sessions = value.ToList(); }
        }

        private List<GameSession> _Sessions;

        public GameSessionListViewModel(StudentUser stu)
        {
            Student = stu;
            ActiveGameSessions();
        }


        public void ExecuteLogoutCommand(object ob)
        {
            App.OnLogout();
        }

        public async void ActiveGameSessions()
        {
            using (var gdrive = new GameDriver())
            {
                var result = await gdrive.GetGameSessionsByStudent(Student.Email);
                _Sessions = result;
            }

            OnPropertyChanged(nameof(Sessions));
        }

        public void ContinueToGameBoard(int id)
        {
            var passthru = _Sessions[id];
            if (passthru != null)
            {
                ContinueGameBoard?.Invoke(this, new GameBoardViewModel(passthru));
            }

        }

        private RelayCommand _LougoutCommand;

        public RelayCommand LogoutCommand
        {
            get { return _LougoutCommand ?? (_LougoutCommand = new RelayCommand(ExecuteLogoutCommand)); }
            set { _LougoutCommand = value; }
        }

        public event EventHandler<Object> ContinueGameBoard;

    }
}
