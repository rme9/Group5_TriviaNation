﻿using System;
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
using TriviaNation.Student.Properties;
using TriviaNation.Student.ViewModels;
using TriviaNation.ViewModels;
using TriviaNation.Views;
using TriviaNation.Student.Views;
using LoginPopupView = TriviaNation.Views.LoginPopupView;
using GameSessionListView = TriviaNation.Student.Views.GameSessionListView;

namespace TriviaNation
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public Views.LoginPopupView _LoginPopup;
        public GameSessionListView _SessionList;

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
			if (e is GameSessionListViewModel model)
			{
				DataContext = model;
                _SessionList = new GameSessionListView(model);
                _SessionList.Show();
			}
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			Application.Current.Shutdown();
		}
	}
}
