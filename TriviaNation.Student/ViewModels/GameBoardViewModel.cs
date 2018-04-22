using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TriviaNation.Util;
using TriviaNation.ViewModels;

namespace TriviaNation.Student.ViewModels
{
    public class GameBoardViewModel : IViewModel
    {
        public string SessionID = null;
        public string LoggedInUserName
        {
            get { return Application.Current.Properties["LoggedInUserName"] as string; }
            set { }
        }

        public GameBoardViewModel(string id)
        {
            SessionID = id;
        }

        //public EventHandler<object> TransitionChildView;

        #region Logout

        public void ExecuteLogoutCommand(object ob)
        {
            App.OnLogout();
        }

        private RelayCommand _LougoutCommand;

        public RelayCommand LogoutCommand
        {
            get { return _LougoutCommand ?? (_LougoutCommand = new RelayCommand(ExecuteLogoutCommand)); }
            set { _LougoutCommand = value; }
        }

        #endregion
    }
}