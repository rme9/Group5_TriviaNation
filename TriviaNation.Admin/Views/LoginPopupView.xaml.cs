using System.Windows;
using TriviaNation.ViewModels;
using Application = System.Windows.Application;

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

			model.LoginComplete += Model_LoginComplete;

			this.Closing += LoginPopupView_Closing;
		}

		private void LoginPopupView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (Application.Current.Properties["LoggedInUserId"] == null)
			{
				Application.Current.Shutdown();
			}
		}

		private void Model_LoginComplete(object sender, object e)
		{
			this.Close();
		}

		
	}
}
