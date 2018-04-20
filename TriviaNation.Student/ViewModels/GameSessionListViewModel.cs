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
        public StudentUser Student = new StudentUser("bob", "bob2");
        public List<string> Sessions;
        public string StudentEmail;
        public string StudentName;

        public GameSessionListViewModel(string name)
        {
            //Student = input;
            StudentName = name;
        }


        public void ExecuteLogoutCommand(object ob)
        {
            App.OnLogout();
        }

        /*public List<string> ReturnAvailableSessions()
        {
            List<string> output = new List<string>();
        }*/

        private RelayCommand _LougoutCommand;

        public RelayCommand LogoutCommand
        {
            get { return _LougoutCommand ?? (_LougoutCommand = new RelayCommand(ExecuteLogoutCommand)); }
            set { _LougoutCommand = value; }
        }

    }
}
