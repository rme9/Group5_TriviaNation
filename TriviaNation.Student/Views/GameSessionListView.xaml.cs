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
using TriviaNation.Core.Models;
using TriviaNation.Student.ViewModels;
using Application = System.Windows.Application;

namespace TriviaNation.Student.Views
{
    /// <summary>
    /// Interaction logic for GameSessionListView.xaml
    /// </summary>
    public partial class GameSessionListView : Window
    {
        public GameSessionListViewModel gsvm;
        //public List<string> Sessions = new List<string>();
        public GameSessionListView(GameSessionListViewModel model)
        {
            InitializeComponent();
            DataContext = model;
            gsvm = model;

            CurrentName.Content = model.Student.Name;
            //this.Show();
            //if (model?.Sessions?.Count > 0)
            //{
            //    foreach (GameSession x in model.Sessions)
            //        AvailableSessions.Items.Add(x.Name);
            //}*/

            //List<string> a= new List<string>() { "1", "2", "3" };
            /*foreach(GameSession x in model.Sessions)
                AvailableSessions.Items.Add(test);*/
            //this.Show();
            this.Closing += GameSessionListView_Closing;
        }

        private void GameSessionListView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Application.Current.Properties["LoggedInUserId"] == null)
            {
                Application.Current.Shutdown();
            }
        }

        private void ContinueButton_OnClick(object sender, RoutedEventArgs e)
        {
            if(AvailableSessions.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Game Session.", "Error - No Selection Found", MessageBoxButton.OK);
            }
            else
            {
                gsvm.ContinueToGameBoard(AvailableSessions.SelectedIndex);
                this.Hide();
                
            }
        }

        public event EventHandler<Object> EnterGame;
    }
}
