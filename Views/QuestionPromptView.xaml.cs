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
using TriviaNation.ViewModels;

namespace TriviaNation.Views
{
    /// <summary>
    /// Interaction logic for QuestionPromptView.xaml
    /// </summary>
    public partial class QuestionPromptView : Window
    {
        public QuestionPromptView(QuestionPromptViewModel vm)
        {
            Random rand = new Random();
            int correct = rand.Next(1,4);
            int[] option = new int[3] { 0, 1, 2};
            int number, temp = 0;

            InitializeComponent();
            DataContext = vm;

            for(int i = 0; i < 3; i++)
            {
                number = rand.Next(0, 2);
                temp = option[i];
                option[i] = option[number];
                option[number] = temp;
            }

            QuestionPane.Content = vm.getQuestionBody();
            if(correct == 1)
            {
                AnswerBox1.Content = vm.getQuestionAnswer();
                AnswerBox2.Content = vm.getQuestionOption(option[0]);
                AnswerBox3.Content = vm.getQuestionOption(option[1]);
                AnswerBox4.Content = vm.getQuestionOption(option[2]);

                AnswerBox1.Tag = "correct";
                AnswerBox2.Tag = "incorrect";
                AnswerBox3.Tag = "incorrect";
                AnswerBox4.Tag = "incorrect";
            }
            else if(correct == 2)
            {
                AnswerBox1.Content = vm.getQuestionOption(option[0]);
                AnswerBox2.Content = vm.getQuestionAnswer();
                AnswerBox3.Content = vm.getQuestionOption(option[1]);
                AnswerBox4.Content = vm.getQuestionOption(option[2]);

                AnswerBox1.Tag = "incorrect";
                AnswerBox2.Tag = "correct";
                AnswerBox3.Tag = "incorrect";
                AnswerBox4.Tag = "incorrect";
            }
            else if(correct == 3)
            {
                AnswerBox1.Content = vm.getQuestionOption(option[0]);
                AnswerBox2.Content = vm.getQuestionOption(option[1]);
                AnswerBox3.Content = vm.getQuestionAnswer();
                AnswerBox4.Content = vm.getQuestionOption(option[2]);

                AnswerBox1.Tag = "incorrect";
                AnswerBox2.Tag = "incorrect";
                AnswerBox3.Tag = "correct";
                AnswerBox4.Tag = "incorrect";
            }
            else if(correct == 4)
            {
                AnswerBox1.Content = vm.getQuestionOption(option[0]);
                AnswerBox2.Content = vm.getQuestionOption(option[1]);
                AnswerBox3.Content = vm.getQuestionOption(option[2]);
                AnswerBox4.Content = vm.getQuestionAnswer();

                AnswerBox1.Tag = "incorrect";
                AnswerBox2.Tag = "incorrect";
                AnswerBox3.Tag = "incorrect";
                AnswerBox4.Tag = "correct";
            }
        }

        private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
        {
            string correct = "Correct!";
            string incorrect = "Incorrect.";
            string caption = "Your answer was: ";
            
            if(AnswerBox1.IsChecked == true && AnswerBox1.Tag.Equals("correct"))
            {
                var result = MessageBox.Show(correct, caption, MessageBoxButton.OK);
                if(result == MessageBoxResult.OK)
                {
                    Close();
                }
            }
            else if(AnswerBox2.IsChecked == true && AnswerBox2.Tag.Equals("correct"))
            {
                var result = MessageBox.Show(correct, caption, MessageBoxButton.OK);
                if (result == MessageBoxResult.OK)
                {
                    Close();
                }
            }
            else if (AnswerBox3.IsChecked == true && AnswerBox3.Tag.Equals("correct"))
            {
                var result = MessageBox.Show(correct, caption, MessageBoxButton.OK);
                if (result == MessageBoxResult.OK)
                {
                    Close();
                }
            }
            else if (AnswerBox4.IsChecked == true && AnswerBox4.Tag.Equals("correct"))
            {
                var result = MessageBox.Show(correct, caption, MessageBoxButton.OK);
                if (result == MessageBoxResult.OK)
                {
                    Close();
                }
            }
            else
            {
                var result = MessageBox.Show(incorrect, caption, MessageBoxButton.OK);
                if (result == MessageBoxResult.OK)
                {
                    Close();
                }
            }
        }
    }
}
