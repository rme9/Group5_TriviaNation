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
using TriviaNation.Properties;
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
