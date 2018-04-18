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
        private GameBoardViewModel game;
        List<Rectangle> territories = new List<Rectangle>();
        public GameBoardView()
        {
            InitializeComponent();
            AddTerritories();
            ResetTerritories();
            InitializeMouseButtons();
        }

        private void showInformation(object sender, RoutedEventArgs e)
        {
            InformationPanel.Visibility = Visibility.Visible;
            Information_Info.Visibility = Visibility.Visible;
            Information_Controlled.Visibility = Visibility.Visible;
            Information_ControlName.Visibility = Visibility.Visible;
            //AttackButton.Visibility = Visibility.Visible;
            //DefendButton.Visibility = Visibility.Visible;

            Information_Info.Content = ((Rectangle)sender).Name;
            if(Information_ControlName.Content.Equals(""))
            {
                Information_ControlName.Content = "Uncontested.";
                AttackButton.Visibility = Visibility.Visible;
            }
        }

        private void AttackButton_OnClick(object sender, RoutedEventArgs e)
        {
            //var createQuestionWindow = new QuestionPromptView(new QuestionPromptViewModel());

            //createQuestionWindow.Show();

        }

        public void AddTerritories()
        {
            foreach (Rectangle x in boardGrid.Children.OfType<Rectangle>())
            {
                if(x.Name != "InformationPanel")
                    territories.Add(x);
            }
        }

        private void InitializeMouseButtons()
        {
            List<Rectangle> test = new List<Rectangle>();
            foreach (Rectangle x in territories)
                x.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
        }

        private void ResetTerritories()
        {
            foreach(Rectangle x in territories)
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
