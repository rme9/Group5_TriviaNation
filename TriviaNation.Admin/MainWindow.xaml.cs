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

		private MainWindowViewModel _ViewModel;

		public MainWindow()
		{
			InitializeComponent();

			_BaseViewModel = new AdminDashboardViewModel();
			_BaseView = new AdminDashboardView(_BaseViewModel);

			_ViewModel = new MainWindowViewModel();
			
			 _ViewModel.ContentViewBox = _BaseView;

			DataContext = _ViewModel;

			this.Show();

			ShowLoginWindow();
		}

		private void ShowLoginWindow()
		{
			var loginvm = new LoginPopupViewModel();

			_LoginPopup = new LoginPopupView(loginvm);

			loginvm.LoginComplete += LoginView_LoginComplete;

			_LoginPopup.Owner = this;

			_LoginPopup.ShowDialog();
		}

		private void LoginView_LoginComplete(object sender, object e)
		{
			_BaseViewModel.UpdateViewAfterLogin();
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			Application.Current.Shutdown();
		}

		private AdminDashboardViewModel _BaseViewModel;
		private AdminDashboardView _BaseView;


		private void CreateGame_OnClick(object sender, RoutedEventArgs e)
		{

			var newContentVM = new GameManagementViewModel();
			var newContent = new GameManagementView(newContentVM);

			newContentVM.CloseView += TransitionToBaseView;

			_ViewModel.ContentViewBox.Content = newContent;
		}

		private void TransitionToBaseView(object sender, EventArgs eventArgs)
		{
			_ViewModel.ContentViewBox = _BaseView;
		}

		private void ManageQuestionBank_OnClick(object sender, RoutedEventArgs e)
		{

		}
	}
}
