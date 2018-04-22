using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaNation.Core.Drivers;
using TriviaNation.Core.Models;
using TriviaNation.Util;
using Application = System.Windows.Application;


namespace TriviaNation.Student.ViewModels
{
    public class GameSessionListViewModel
    {
        public StudentUser Student;
        public List<GameSession> Sessions = new List<GameSession>();
        private GameDriver gdrive = new GameDriver();
        public WebServiceDriver wdrive = new WebServiceDriver();

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
            var result = new GameSession("abc"); //await wdrive.GetGameSessionsByInstructor("beddy@uwf.edu");
            /*if (result == null)
            {
                Sessions.Add(new GameSession("abc"));
            }
            else
            {
                foreach (GameSession x in result)
                    Sessions.Add(x);
            }*/
            Sessions.Add(result);
        }

        /*public List<string> ReturnAvailableSessions()
        {
            List<string> output = new List<string>();
        }*/

        public void ContinueToGameBoard(string id)
        {
            ContinueGameBoard?.Invoke(this, new GameBoardViewModel(id));
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
