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

namespace TriviaNation.UI.Views
{
    /// <summary>
    /// Interaction logic for GameCreationView.xaml
    /// </summary>
    public partial class GameCreationView : Window
    {
        public GameCreationView()
        {
            InitializeComponent();
        }

	    public void OnClick_Cancel(object sender, RoutedEventArgs e)
	    {
		    this.Close();
	    }

    }
}
