using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Amazon.Auth.AccessControlPolicy.ActionIdentifiers;
using TriviaNation.Drivers;
using TriviaNation.ViewModels;

namespace TriviaNation.Views
{
	/// <summary>
	/// Interaction logic for LoginPopupView.xaml
	/// </summary>
	public partial class LoginPopupView : Window
    {

		public LoginPopupView(LoginPopupViewModel model)
		{
			InitializeComponent();

			DataContext = model;
		    ResetLogin();
		}

		#region Login Complete
		// TODO This code may actually belong in the ViewModel

		private void OnClicked_ContinueButton(object sender, RoutedEventArgs e)
		{
            
		    if (UserEntry.Text.Equals(""))
		    {
		        ErrorBox.Visibility = Visibility.Visible;
		        ErrorBox.Text = "Error: Username cannot be empty.";
                ResetLogin();
		    }
            else if (PasswordEntry.Password.Equals(""))
            {
                ErrorBox.Visibility = Visibility.Visible;
                ErrorBox.Text = "Error: Password cannot be empty.";
                ResetLogin();
            }
            else
            {
                LoginComplete?.Invoke(this, new GameBoardViewModel());
                this.Hide();
            }
		}

        public void ResetLogin()
        {
            UserEntry.Text = "";
            PasswordEntry.Password = "";
        }

		public event EventHandler<Object> LoginComplete;

		#endregion
	}
}
