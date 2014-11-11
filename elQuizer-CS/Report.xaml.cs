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
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        public static List<string> userAnswers = new List<string>();
        string lines;
        public Report()
        {
            InitializeComponent();
            fillTable();
        }

        private void fillTable()
        {
            lines = "";
            for (int i = 0; i < userAnswers.Count; i++)
			{
                table_sp.Children.Add(makeQuestionGrid(i));
			}
        }

        Grid makeQuestionGrid(int index)
        {
            Grid grid = new Grid();
            grid.Margin = new Thickness(0, 0, 0, 3);
            grid.Height = 27;
            Label[] labels = new Label[3];
            for (int i = 0; i < 3; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                if (i != 0)
                {
                    cd.Width = GridLength.Auto;
                }
                grid.ColumnDefinitions.Add(cd);
                labels[i] = new Label();
                labels[i].VerticalContentAlignment = VerticalAlignment.Center;
                labels[i].HorizontalContentAlignment = HorizontalAlignment.Center;
                
                Grid.SetColumn(labels[i], i);
                grid.Children.Add(labels[i]);
            }

            labels[0].Content = QuestionBank.questions[index].getQuestion();
            labels[1].Content = QuestionBank.questions[index].getAnswer();
            labels[2].Content = userAnswers[index];

            bool isCorrect;
            if (QuestionBank.questions[index].checkAnswer(
                labels[2].Content.ToString())) {
                labels[2].Background = new SolidColorBrush(Practice.successColor);
                isCorrect = true;
            }
            else {
                labels[2].Background = new SolidColorBrush(Practice.failurColor);
                isCorrect = false;
            }
            lines += labels[0].Content.ToString() + ',' +
                     labels[1].Content.ToString() + ',' +
                     labels[2].Content.ToString() + ',' +
                     (isCorrect?"Correct":"Wrong") + '\n';

            return grid;
        }

        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Elfile.saveAsText(lines))
            {
                MessageBox.Show("File saved.");
            } 
            
        }

        private void home_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
