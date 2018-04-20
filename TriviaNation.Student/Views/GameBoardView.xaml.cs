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


namespace TriviaNation.Student.Views
{
    /// <summary>
    /// Interaction logic for GameBoardView.xaml
    /// </summary>
    public partial class GameBoardView : UserControl
    {
        List<Rectangle> territories = new List<Rectangle>();

        public GameBoardView()
        {
            InitializeComponent();
            AddFunctionality();
        }

        private void AddFunctionality()
        {
            foreach (Rectangle x in boardGrid.Children.OfType<Rectangle>())
            {
                if (x.Name != "Information_Panel")
                {
                    territories.Add(x);
                    x.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
                    x.Opacity = 0;
                }
            }

        }

        private void AttackButton_onClick(object sender, RoutedEventArgs e)
        {

        }

        private void showInformation(object sender, RoutedEventArgs e)
        {
            Information_Panel.Visibility = Visibility.Visible;
            Information_Info.Visibility = Visibility.Visible;
            Information_Controlled.Visibility = Visibility.Visible;
            Information_ControlName.Visibility = Visibility.Visible;
            //AttackButton.Visibility = Visibility.Visible;
            //DefendButton.Visibility = Visibility.Visible;

            Information_Info.Content = ((Rectangle)sender).Name;
            if (Information_ControlName.Content.Equals(""))
            {
                Information_ControlName.Content = "Uncontested.";
                AttackButton.Visibility = Visibility.Visible;
            }
            else
            {
                DefendButton.Visibility = Visibility.Visible;
            }
        }

        private void ResetTerritories()
        {
            foreach (Rectangle x in territories)
            {
                x.Opacity = 0;
            }
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ResetTerritories();
            ((Rectangle)sender).Opacity = 1;
            ((Rectangle)sender).Fill = new SolidColorBrush(Colors.Blue);
            ((Rectangle)sender).Fill.Opacity = .3;
            ((Rectangle)sender).StrokeThickness = 3;
            showInformation(((Rectangle)sender), e);
        }
    }
}
