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
            question_txt.Text = QuestionBank.questions[currQuestionIndex].getQuestion();

            question_message_txt.Text = QuestionBank.questions[currQuestionIndex].getQuestionTypeValue().ToString();

            question_order_txt.Content = "Answer";
            //<TextBox x:Name="mutli_txt" Grid.Column="1" VerticalAlignment="Stretch" MaxLength="100" VerticalContentAlignment="Center" Margin="0,0,7,0"></TextBox>

        }

        private void action_btn_Click(object sender, RoutedEventArgs e)
        {
            Color[] col = new Color[2];
            //col[0] = Colors.Red;
            col[0] = Color.FromArgb(0xFF, 0xCB, 0x36, 0x36);
            //col[1] = Colors.Green;
            col[1] = Color.FromArgb(0xFF, 0x36, 0xCB, 0x3C);
            Random rand = new Random();
            rects[currQuestionIndex].Fill = new SolidColorBrush(col[rand.Next(2)]);
            currQuestionIndex++;
            showQuestion(currQuestionIndex);

        }

    }
}
