using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace elQuizer_CS
{
    /// <summary>
    /// Interaction logic for AddNewQuestion.xaml
    /// </summary>
    public partial class AddNewQuestion : Window
    {
        public AddNewQuestion()
        {
            InitializeComponent();
        }

        private void question_txt_TextChanged(object sender, 
                                              TextChangedEventArgs e)
        {
            FillTheBlank();
        }

        private void FillTheBlank()
        {
            blanks_wrap.Children.RemoveRange(0, blanks_wrap.Children.Count);
            string[] words = question_txt.Text.Split(' ');
            foreach (string item in words)
	        {
                if(item.Equals("")) continue;
                ToggleButton tb = new ToggleButton();
                tb.Content = "  " + item + "  ";
                tb.Background = new SolidColorBrush(Colors.White);
                tb.Margin = new Thickness(2);
                tb.Height = 27;
                tb.Click += tb_Click;
                blanks_wrap.Children.Add(tb);
	        }
            
        }

        void tb_Click(object sender, RoutedEventArgs e)
        {
            foreach (ToggleButton item in blanks_wrap.Children)
            {
                item.IsChecked = false;
            }
            ((ToggleButton)sender).IsChecked = true;
        }

        private void mutli_btn_Click(object sender, RoutedEventArgs e)
        {
            if (choices_sp.Children.Count == 5)
            {
                MessageBox.Show("You can only have up to 5 choices.");
                return;
            }
            if (!isValidChoice(mutli_txt.Text))
            {
                MessageBox.Show("Choices must be unique.");
                mutli_txt.SelectAll();
                return;
            }
            RadioButton NewChoice = new RadioButton();
            NewChoice.Margin = new Thickness(0, 0, 0, 7);
            NewChoice.Content = mutli_txt.Text.Trim();
            choices_sp.Children.Add(NewChoice);
            mutli_txt.Text = "";
            NewChoice.MouseRightButtonDown += NewChoice_MouseRightButtonDown;
        }

        bool isValidChoice(string choice)
        {
            choice = choice.ToLower().Trim();
            foreach (var item in choices_sp.Children)
            {
                if (((RadioButton)item).Content.ToString().ToLower() == choice)
                {
                    return false;
                }
            }
            return true;
        }

        void NewChoice_MouseRightButtonDown(object sender,
                                            MouseButtonEventArgs e)
        {
            mutli_txt.Text = ((RadioButton)sender).Content.ToString();
            choices_sp.Children.Remove((RadioButton)sender);

        }

        private void mutli_key_down(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                mutli_btn_Click(sender, null);
            }
        }

        private void add_question_btn_Click(object sender, RoutedEventArgs e)
        {
            if (question_txt.Text == "")
            {
                MessageBox.Show("Where is the question?!");
                return;
            }
            switch (((TabItem)question_tabs.SelectedItem).Header.ToString())
            {
                case "Short Answer":
                    QuestionBank.questions.Add(
                        new ShortAnswerQuestion(question_txt.Text,
                                                short_answer_txt.Text));
                    break;
                case "Fill The Blank":
                    int tokenSelected = getSelectedToken();
                    if (tokenSelected == -1) {
                        MessageBox.Show("Select the word to be filled.");
                        return;
                    }
                    QuestionBank.questions.Add(
                        new FillTheBlankQuestion(question_txt.Text,
                                                 tokenSelected));
                    break;
                case "Mutli Choice":
                    if (choices_sp.Children.Count < 2) {
                        MessageBox.Show("You must have 2 choices at least.");
                        return;
                    }
                    int choiceSelected = getSelectedToken();
                    if (choiceSelected == -1) {
                        MessageBox.Show(
                            "Select the correct choice for the question");
                        return;
                    }
                    QuestionBank.questions.Add(
                        new MutliChoiceQuestion(question_txt.Text,getChoices(),                                     getSelectedChoice()));
                    break;
                case "True False":
                    QuestionBank.questions.Add(
                        new TrueFalseQuestion(question_txt.Text, 
                                              getTrueOrFalse()));
                    break;
                default:
                    break;
            }
        }

        int getSelectedToken() {

            for (int i = 0; i < blanks_wrap.Children.Count; i++)
            {
                if (((ToggleButton)blanks_wrap.Children[i]).IsChecked == true)
                {
                    return i;
                }
            }
            return -1;
        }

        List<string> getChoices()
        {
            List<string> choices = new List<string>();
            foreach (RadioButton item in choices_sp.Children)
            {
                choices.Add(item.Content.ToString());
            }
            return choices;
        }

        int getSelectedChoice()
        {
            List<string> choices = new List<string>();
            for (int i = 0; i < choices_sp.Children.Count; i++)
			{
			    if(((RadioButton)choices_sp.Children[i]).IsChecked == true) {
                    return i ;
                }
			}            
            return -1;
        }

        bool getTrueOrFalse()
        {
            if (true_rb.IsChecked == true) return true;
            return false;
        }

        
    }
}
