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

namespace TriviaNation.Student.Views
{
    /// <summary>
    /// Interaction logic for QuestionPromptView.xaml
    /// </summary>
    public partial class QuestionPromptView : Window
    {
        private static int _CorrectAnswer;
        public QuestionPromptView(QuestionPromptViewModel input)
        {
            InitializeComponent();
            DataContext = input;

            SetUpQuestions(input);
            SubmitButton.Click += this.CheckCorrect;
            this.Show();
        }

        private void CheckCorrect(object sender, RoutedEventArgs e)
        {
            int val = 6;
            if (Answer1.IsChecked == true)
                val = 0;
            else if (Answer2.IsChecked == true)
                val = 1;
            else if (Answer3.IsChecked == true)
                val = 2;
            else if (Answer4.IsChecked == true)
                val = 3;

            if (_CorrectAnswer == val)
            {
                MessageBox.Show("Correct", "Result", MessageBoxButton.OK);
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect", "Result", MessageBoxButton.OK);
                this.Close();
            }


        }

        public void SetUpQuestions(QuestionPromptViewModel qpvm)
        {
            int[] answers = new int[4]{ 0, 1, 2, 3};
            Random rand = new Random();
            int number, temp = 0;

            for (int i = 0; i < 4; i++)
            {
                number = rand.Next(0, 3);
                temp = answers[i];
                answers[i] = answers[number];
                answers[number] = temp;
            }

            for(int x = 0; x < answers.Count(); x++)
            {
                if (answers[x] == 0)
                    _CorrectAnswer = x;
            }

            QuestionBody.Content = qpvm.GetQuestionBody();
            Answer1.Content = qpvm.GetQuestionAnswer(answers[0]);
            Answer2.Content = qpvm.GetQuestionAnswer(answers[1]);
            Answer3.Content = qpvm.GetQuestionAnswer(answers[2]);
            Answer4.Content = qpvm.GetQuestionAnswer(answers[3]);
        }
    }
}
