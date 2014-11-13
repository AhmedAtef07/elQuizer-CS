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
    public partial class Quiz : BaseWindow
    {        
        public Quiz()
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
                isFinished = true;
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
            rects[currQuestionIndex].Fill = new SolidColorBrush(ElTools.successColor);
           
        }
        void falseAnswer()
        {
            rects[currQuestionIndex].Fill = new SolidColorBrush(ElTools.failurColor);
        }
        

        

    }
}
