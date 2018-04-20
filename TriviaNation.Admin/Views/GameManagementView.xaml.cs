using System.Windows.Controls;
using TriviaNation.ViewModels;

namespace TriviaNation.Views
{
	/// <summary>
	/// Interaction logic for GameManagementView.xaml
	/// </summary>
	public partial class GameManagementView : UserControl
	{
		public GameManagementView(GameManagementViewModel vm)
		{
			InitializeComponent();

			DataContext = vm;
		}
	}
}
