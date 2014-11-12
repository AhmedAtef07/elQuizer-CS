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
    /// Interaction logic for Quiz.xaml
    /// </summary>
    public partial class Quiz : Window
    {
        // Red.
        Color failurColor = ElTools.failurColor;
        // Green.
        Color successColor = ElTools.successColor;

        int questionsCount,
            currQuestionIndex;
        Rectangle[] rects;
        TextBox textAnswer;
        StackPanel choices_sp;
        Question currQuestion;
        bool quizFinished;
        public Quiz()
        {
            InitializeComponent();
            this.Closing += new System.ComponentModel.CancelEventHandler(
                MyWindow_Closing);
            prepareStage();
        }

        private void MyWindow_Closing(object sender, 
            System.ComponentModel.CancelEventArgs e)
        {
            if (!quizFinished)
            {
                MessageBoxResult result =  MessageBox.Show(
                    "You really want to close?", "Escaping?",
                    MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;                
                }                
            }
        }
        
        void prepareStage()
        {
            ElTools.shuffleQuestions();
            Report.userAnswers.Clear();
            Report.userBooleans.Clear();
            currQuestionIndex = 0;
            questionsCount = ElTools.shuffledQuestions.Count;
            action_btn.Tag = "0";
            quizFinished = false;
            setProgressGrid();
            showQuestion(0);
        }

        void setProgressGrid()
        {
            // questionsCount = 30;
            rects = new Rectangle[questionsCount];

            for (int i = 0; i < questionsCount; ++i)
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
            currQuestion = ElTools.shuffledQuestions[currQuestionIndex];
            question_txt.Text = currQuestion.getQuestion();

            question_message_txt.Text = currQuestion.getMessage();

            answer_grid.Children.RemoveRange(1, answer_grid.Children.Count - 1);
            switch (currQuestion.getAnswerType())
            {
                case Question.AnswerType.Text:
                    question_order_txt.Content = "Answer:";

                    textAnswer = new TextBox();
                    textAnswer.VerticalAlignment = VerticalAlignment.Stretch;
                    textAnswer.MaxLength = 100;
                    textAnswer.Height = 27;
                    textAnswer.VerticalContentAlignment = (
                        VerticalAlignment.Center);
                    textAnswer.Margin = new Thickness(0, 0, 7, 0);
                    Grid.SetColumn(textAnswer, 1);
                    answer_grid.Children.Add(textAnswer);
                    textAnswer.Focus();
                    break;
                case Question.AnswerType.Choice:
                    question_order_txt.Content = "Choose:";

                    choices_sp = new StackPanel();
                    choices_sp.VerticalAlignment = VerticalAlignment.Center;
                    choices_sp.Margin = new Thickness(10, 10, 0, 0);
                    List<string> choices = new List<string>();
                    switch (currQuestion.getQuestionType())
	                {
                        case Question.QuestionType.TrueFalse:
                            choices = ((TrueFalseQuestion)
                                currQuestion).getChoices();
                            break;
                        case Question.QuestionType.MutliChoice:
                            choices = ((MutliChoiceQuestion)
                                currQuestion).getShuffledChoices();
                            break;
                        default:
                            break;
	                }                     
                    for (int i = 0; i < choices.Count; ++i)
                    {
                        RadioButton NewChoice = new RadioButton();
                        NewChoice.Margin = new Thickness(0, 0, 0, 7);
                        NewChoice.Content = choices[i];
                        choices_sp.Children.Add(NewChoice);                       
                    }
                        
                    Grid.SetColumn(choices_sp, 1);
                    answer_grid.Children.Add(choices_sp);
                    choices_sp.Children[0].Focus();
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
                    falseAnswer();
                    Report.userBooleans.Add(false);
                    break;
                case 1:
                    trueAnswer();
                    Report.userBooleans.Add(true);
                    break;
                default:
                    break;
            }
            currQuestionIndex++;

            if (currQuestionIndex == questionsCount)
            {
                quizFinished = true;
                Report report = new Report();
                this.Close();
                report.ShowDialog();
                return;
            }

            if (currQuestionIndex + 1 == questionsCount)
            {
                action_btn.Content = "Submit and Check Correct Answers (Ctrl + ↵)";                    
            }
            
            showQuestion(currQuestionIndex);
        }
        void trueAnswer()
        {
            rects[currQuestionIndex].Fill = new SolidColorBrush(successColor);
           
        }
        void falseAnswer()
        {
            rects[currQuestionIndex].Fill = new SolidColorBrush(failurColor);
        }
        int checkAnswer()
        {
            string answer;
            switch (currQuestion.getAnswerType())
            {
                case Question.AnswerType.Text:
                    if (textAnswer.Text == "")
                    {
                        MessageBox.Show("Where is the answer?");
                        return -1;
                    }
                    answer = textAnswer.Text.Trim();
                    Report.userAnswers.Add(answer);
                    return currQuestion.checkAnswer(answer) ? 1 : 0;
                case Question.AnswerType.Choice:                    
                    switch (currQuestion.getQuestionType())
	                {
                        case Question.QuestionType.TrueFalse:
                            for (int i = 0; i < choices_sp.Children.Count; ++i)
                            {
                                if (((RadioButton)choices_sp.Children[i])
                                    .IsChecked == true)
                                {
                                    answer = ((RadioButton)choices_sp.
                                        Children[i]).Content.ToString();
                                    Report.userAnswers.Add(answer);
                                    return currQuestion.checkAnswer(answer
                                        == "True")?1:0;
                                }
                            }
                            MessageBox.Show("Pick a choice!");
                            return -1;
                        case Question.QuestionType.MutliChoice:
                            RadioButton selectedRB = getCheckedRadioButton();
                            if (selectedRB != null)
                            {
                                answer = selectedRB.Content.ToString();
                                Report.userAnswers.Add(answer);
                                return currQuestion.checkAnswer(answer)?1:0;
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

        RadioButton getCheckedRadioButton()
        {
            for (int i = 0; i < choices_sp.Children.Count; ++i)
            {
                if (((RadioButton)choices_sp.Children[i]).IsChecked == true)
                {
                    return (RadioButton)choices_sp.Children[i];
                }
            }
            return null;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return &&
                (Keyboard.Modifiers & ModifierKeys.Control) ==
                ModifierKeys.Control)
            {
                action_btn_Click(null, null);
            }
        }

    }
}
