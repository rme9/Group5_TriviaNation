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

		}

		#region Login Complete
		// TODO This code may actually belong in the ViewModel

		private void OnClicked_ContinueButton(object sender, RoutedEventArgs e)
		{
			if (User.IsChecked ?? false)
			{
				// TODO Load User View
			}
			else if (Admin.IsChecked ?? false)
			{
				LoginComplete?.Invoke(this, new AdminViewModel());
			}

			this.Hide();
		}

		public event EventHandler<Object> LoginComplete;

		#endregion
	}
}
