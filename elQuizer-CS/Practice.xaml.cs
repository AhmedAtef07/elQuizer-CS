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

namespace elQuizer_CS
{
    /// <summary>
    /// Interaction logic for Practice.xaml
    /// </summary>
    public partial class Practice : Window
    {
        int questionsCount,
            currQuestionIndex;
        Rectangle[] rects;
        TextBox textAnswer;
        StackPanel choices_sp;
        public Practice()
        {
            InitializeComponent();
            this.Closing += new System.ComponentModel.CancelEventHandler(
                MyWindow_Closing);
            prepareStage();
        }

        private void MyWindow_Closing(object sender, 
            System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result =  MessageBox.Show(
                "You really want to close?", "Escaping?",
                MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;                
            }
        }

        void prepareStage()
        {
            currQuestionIndex = 0;
            questionsCount = QuestionBank.questions.Count;
            setProgressGrid();
            showQuestion(0);
        }

        void setProgressGrid()
        {
            // questionsCount = 30;
            rects = new Rectangle[questionsCount];

            for (int i = 0; i < questionsCount; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                progress_grid.ColumnDefinitions.Add(cd);

                rects[i] = new Rectangle();
                rects[i].Fill = new SolidColorBrush(Colors.LightGray);
                rects[i].Height = 17;
                if(i != 0) rects[i].Margin = new Thickness(3, 0, 0, 0);

                Grid.SetColumn(rects[i], i);
                progress_grid.Children.Add(rects[i]);
            }

        }
        void showQuestion(int questionIndex)
        {
            Question question = QuestionBank.questions[currQuestionIndex];
            question_txt.Text = question.getQuestion();

            question_message_txt.Text = question.getMessage();

            answer_grid.Children.RemoveAt(1);
            switch (question.getAnswerType())
            {
                case Question.AnswerType.Text:
                    question_order_txt.Content = "Answer:";

                    textAnswer = new TextBox();
                    textAnswer.VerticalAlignment = VerticalAlignment.Stretch;
                    textAnswer.MaxLength = 100;
                    textAnswer.VerticalContentAlignment = VerticalAlignment.Center;
                    textAnswer.Margin = new Thickness(0, 0, 7, 0);
                    Grid.SetColumn(textAnswer, 1);
                    answer_grid.Children.Add(textAnswer);
                    break;
                case Question.AnswerType.Choice:
                    question_order_txt.Content = "Choose:";

                    choices_sp = new StackPanel();
                    choices_sp.VerticalAlignment = VerticalAlignment.Center;
                    choices_sp.Margin = new Thickness(10, 10, 0, 0);
                    List<string> choices = new List<string>();
                    switch (question.getQuestionType())
	                {
                        case Question.QuestionType.TrueFalse:
                            choices = ((TrueFalseQuestion)question).getChoices();
                            break;
                        case Question.QuestionType.MutliChoice:
                            choices = ((MutliChoiceQuestion)question).getShuffledChoices();
                            break;
                        default:
                            break;
	                }                     
                    for (int i = 0; i < choices.Count; i++)
                    {
                        RadioButton NewChoice = new RadioButton();
                        NewChoice.Margin = new Thickness(0, 0, 0, 7);
                        NewChoice.Content = choices[i];
                        choices_sp.Children.Add(NewChoice);
                    }
                        
                    Grid.SetColumn(choices_sp, 1);
                    answer_grid.Children.Add(choices_sp);
                    break;
                default:
                    break;
            }
            
            
        }

        private void action_btn_Click(object sender, RoutedEventArgs e)
        {
            int answerState = checkAnswer();
            switch (answerState)
            {
                case -1:
                    return;
                case 0:
                    // Red.
                    rects[currQuestionIndex].Fill = new SolidColorBrush(Color.FromArgb(0xFF, 0xCB, 0x36, 0x36));
                    break;
                case 1:
                    // Green.
                    rects[currQuestionIndex].Fill = new SolidColorBrush(Color.FromArgb(0xFF, 0x36, 0xCB, 0x3C));
                    break;
                default:
                    break;
            }
            currQuestionIndex++;
            if (currQuestionIndex == questionsCount)
            {
                ((Button)sender).IsEnabled = false;
            }
            else
            {
                showQuestion(currQuestionIndex);

            }

        }
        int checkAnswer()
        {
            //QuestionBank.questions[currQuestionIndex].checkAnswer()
            Question question = QuestionBank.questions[currQuestionIndex];
            switch (question.getAnswerType())
            {
                case Question.AnswerType.Text:
                    if (textAnswer.Text == "")
                    {
                        MessageBox.Show("Where is the answer?");
                        return -1;
                    }
                    return question.checkAnswer(textAnswer.Text)?1:0;
                case Question.AnswerType.Choice:                    
                    switch (question.getQuestionType())
	                {
                        case Question.QuestionType.TrueFalse:
                            for (int i = 0; i < choices_sp.Children.Count; i++)
                            {
                                if (((RadioButton)choices_sp.Children[i]).IsChecked == true)
                                {
                                    return question.checkAnswer(((RadioButton)choices_sp.Children[i]).Content.ToString() == "True")?1:0;
                                }
                            }
                            MessageBox.Show("Pick a choice!");
                            return -1;
                        case Question.QuestionType.MutliChoice:
                            for (int i = 0; i < choices_sp.Children.Count; i++)
                            {
                                if (((RadioButton)choices_sp.Children[i]).IsChecked == true)
                                {
                                    return question.checkAnswer(((RadioButton)choices_sp.Children[i]).Content.ToString())?1:0;
                                }
                            }
                            MessageBox.Show("Pick a choice!");
                            return -1;
                        default:
                            break;
	                }                     
                    
                    break;
                default:
                    break;
            }
            return -1;
        }

    }
}
