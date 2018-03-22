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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TriviaNation.ViewModels;
using TriviaNation.Views;

namespace TriviaNation
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public LoginPopupView _LoginPopup;

		public MainWindow()
		{
			InitializeComponent();

			this.Hide();

			ShowLoginWindow();
		}

		private void ShowLoginWindow()
		{
			var login = new LoginPopupViewModel();

			var _LoginPopup = new LoginPopupView(login);

			_LoginPopup.LoginComplete += LoginView_LoginComplete;

			_LoginPopup.Show();
		}

		private void LoginView_LoginComplete(object sender, object e)
		{
			if (e is AdminViewModel model)
			{
				DataContext = model;
			}

			this.Show();
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			Application.Current.Shutdown();
		}
	}
}
