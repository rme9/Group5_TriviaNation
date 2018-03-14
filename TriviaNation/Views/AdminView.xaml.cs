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
using TriviaNation.UI.Views;
using TriviaNation.ViewModels;

namespace TriviaNation.Views
{
	/// <summary>
	/// Interaction logic for AdminView.xaml
	/// </summary>
	public partial class AdminView : UserControl
	{
		public AdminView()
		{
			InitializeComponent();
		}

		private void CreateGame_OnClick(object sender, RoutedEventArgs e)
		{
			var createGameWindow = new GameCreationView();

			createGameWindow.DataContext = new GameCreationViewModel();

			createGameWindow.Show();
		}
	}
}
