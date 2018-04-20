using System;
using System.Windows;
using TriviaNation.ViewModels;
using TriviaNation.Views;
using LoginPopupView = TriviaNation.Views.LoginPopupView;

namespace TriviaNation
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public Views.LoginPopupView _LoginPopup;

		public MainWindow()
		{
			InitializeComponent();

			ShowLoginWindow();
		}

		private void ShowLoginWindow()
		{
			this.Hide();

			var loginvm = new LoginPopupViewModel();

			_LoginPopup = new LoginPopupView(loginvm);

			loginvm.LoginComplete += LoginView_LoginComplete;

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
