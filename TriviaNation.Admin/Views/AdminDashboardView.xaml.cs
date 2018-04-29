using System.Windows.Controls;
using TriviaNation.ViewModels;

namespace TriviaNation.Views
{
	/// <summary>
	/// Interaction logic for AdminDashboardView.xaml
	/// </summary>
	public partial class AdminDashboardView : UserControl
	{
		public AdminDashboardView(AdminDashboardViewModel vm)
		{
			InitializeComponent();

			DataContext = vm;
		}
	}
}
