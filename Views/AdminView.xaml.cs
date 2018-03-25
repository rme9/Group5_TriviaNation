using System.Windows;
using System.Windows.Controls;
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
			var createGameWindow = new GameCreationView(new GameCreationViewModel());
			
			createGameWindow.Show();
		}
	}
}
