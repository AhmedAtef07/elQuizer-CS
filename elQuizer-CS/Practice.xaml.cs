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
    public partial class Practice : BaseWindow
    {        
        public Practice()
        {
            InitializeComponent();

            base.question_txt = question_txt;
            base.question_message_txt = question_message_txt;
            base.answer_grid = answer_grid;
            base.question_order_txt = question_order_txt;
            base.action_btn = action_btn;
            base.progress_grid = progress_grid;

            this.Closing += new System.ComponentModel.CancelEventHandler(
                MyWindow_Closing);
            prepareStage();
        }      
        
       

        protected override void action_btn_Click(object sender, RoutedEventArgs e)
        {
            if (action_btn.Tag.ToString() == "0")
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
                // Must change the tag here in case the answer was not valied.
                action_btn.Tag = "1";

               
                if (currQuestionIndex + 1 == questionsCount)
                {
                    action_btn.Content = "Finish and Check Results (Ctrl + ↵)";
                    isFinished = true;
                }
                else
                {
                    action_btn.Content = "Next Question";
                }
                action_btn.Focus();

            }
            else
            {                
                currQuestionIndex++;
                if (currQuestionIndex == questionsCount)
                {
                    // New Window
                    Report report = new Report();
                    this.Close();
                    report.ShowDialog();
                    return;
                }
                else
                {
                    showQuestion(currQuestionIndex);
                    action_btn.Content = "Check Answer (Ctrl + ↵)";
                }
                action_btn.Tag = "0";
            }

        }
        void trueAnswer()
        {
            rects[currQuestionIndex].Fill = new SolidColorBrush(ElTools.successColor);
            switch (currQuestion.getAnswerType())
            {
                case Question.AnswerType.Text:
                    textAnswer.Background = new SolidColorBrush(ElTools.successColor);
                    break;
                case Question.AnswerType.Choice:
                    RadioButton radio = getCheckedRadioButton();
                    radio.Background = new SolidColorBrush(ElTools.successColor);
                    radio.Foreground = new SolidColorBrush(ElTools.successColor); 
                    break;
                default:
                    break;
            }
           
        }
        void falseAnswer()
        {
            rects[currQuestionIndex].Fill = new SolidColorBrush(ElTools.failurColor);
            switch (currQuestion.getAnswerType())
            {
                case Question.AnswerType.Text:
                    textAnswer.Background = new SolidColorBrush(ElTools.failurColor);
                    showCorrectTextAnswer();
                    break;
                case Question.AnswerType.Choice:
                    RadioButton checkedRadio = getCheckedRadioButton();
                    checkedRadio.Background = new SolidColorBrush(ElTools.failurColor);
                    checkedRadio.Foreground = new SolidColorBrush(ElTools.failurColor); 
                    RadioButton correctRadio = getCorrectAnswerRadioButton();
                    correctRadio.Background = 
                        new SolidColorBrush(ElTools.successColor);
                    correctRadio.Foreground = 
                        new SolidColorBrush(ElTools.successColor); 
                    break;
                default:
                    break;
            }
        }
        void showCorrectTextAnswer()
        {
            Label label = new Label();
            label.Content = "Correct Answer:";
            Grid.SetRow(label, 1);
            answer_grid.Children.Add(label);

            TextBox rightAnswer = new TextBox();
            rightAnswer.Text = currQuestion.getAnswer().ToString();
            rightAnswer.Background = new SolidColorBrush(ElTools.successColor);
            rightAnswer.VerticalAlignment = VerticalAlignment.Stretch;
            rightAnswer.VerticalContentAlignment = (
                VerticalAlignment.Center);
            rightAnswer.Margin = new Thickness(0, 3, 7, 0);
            rightAnswer.Height = 27;
            Grid.SetColumn(rightAnswer, 1);
            Grid.SetRow(rightAnswer, 1);
            answer_grid.Children.Add(rightAnswer);
        }

        

    }
}
