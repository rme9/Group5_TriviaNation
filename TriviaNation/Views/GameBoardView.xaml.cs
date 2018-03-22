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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class GameBoardView : UserControl
    {
        public GameBoardView()
        {
            InitializeComponent();
        }

        private void showInformation(object sender, RoutedEventArgs e)
        {
            if (((RadioButton)sender).IsChecked == true)
            {
                InformationPanel.Visibility = Visibility.Visible;
                Information_Info.Visibility = Visibility.Visible;
                Information_Controlled.Visibility = Visibility.Visible;
                AttackButton.Visibility = Visibility.Visible;
                DefendButton.Visibility = Visibility.Visible;

                Information_Info.Content = ((RadioButton)sender).Name;
            }
        }
    }
}
