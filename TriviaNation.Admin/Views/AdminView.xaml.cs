using System;
using System.Windows;
using System.Windows.Controls;
using TriviaNation.Models;
using TriviaNation.UI.Views;
using TriviaNation.ViewModels;

namespace TriviaNation.Views
{
	/// <summary>
	/// Interaction logic for AdminView.xaml
	/// </summary>
	public partial class AdminView : UserControl
	{
		private AdminDashboardViewModel _baseViewModel;
		private AdminDashboardView _BaseView;

		public AdminView()
		{
			_baseViewModel = new AdminDashboardViewModel();
			_BaseView = new AdminDashboardView(_baseViewModel);

			InitializeComponent();
			
			ContentViewBox.Child = _BaseView;
		}

		
		private void CreateGame_OnClick(object sender, RoutedEventArgs e)
		{

			var newContentVM = new GameManagementViewModel();
			var newContent = new GameManagementView(newContentVM);

			newContentVM.CloseView += TransitionToBaseView;

			ContentViewBox.Child = newContent;
		}

		private void TransitionToBaseView(object sender, EventArgs eventArgs)
		{
			// TODO Create a base display for the admin view
			ContentViewBox.Child = _BaseView;
		}

		private void ManageQuestionBank_OnClick(object sender, RoutedEventArgs e)
		{
			
		}
	}
}
