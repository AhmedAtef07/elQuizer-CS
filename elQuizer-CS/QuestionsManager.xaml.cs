using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for QuestionsManager.xaml
    /// </summary>
    public partial class QuestionsManager : Window
    {
        public QuestionsManager()
        {
            InitializeComponent();
            fillQuestionList();
        }
        List<string> lines;
        void fillQuestionList()
        {
            lines = new List<string>();
            foreach (var question in QuestionBank.questions)
            {
                //string s = "";
                //s += item.ToString() + '\n';
                //s += item.getQuestion() + '\n';
                //s += item.getAnswer().ToString();
                //question_list.Items.Add(s);

                question_list.Items.Add(question.getFileLineString());
            }
            // Change that later not to take form the listbox.
            lines = question_list.Items.OfType<string>().ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {                    
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Question Bank|*.qbank";
            saveFileDialog.Title = "Save Question Bank";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                File.WriteAllText(saveFileDialog.FileName, 
                    string.Join("\n",lines.ToArray()));
                MessageBox.Show("File Saved."); 
            }
        }  

        private void load_btn_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Question Bank|*.qbank";
            openFileDialog.Title = "Open Question Bank";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                lines = File.ReadAllLines(openFileDialog.FileName).ToList();
                parseLines();
                MessageBox.Show("File loaded.");
                fillQuestionList();
            }
        }
        void parseLines()
        {
            foreach (var line in lines)
            {
                string[] tokens = line.Split(',');
                int firstChar = line[0] - '0';
                Question.QuestionType quesionType;
                quesionType = (Question.QuestionType)firstChar;
                switch (quesionType)
                {
                    case Question.QuestionType.ShortAnswer:
                        QuestionBank.questions.Add(
                            new ShortAnswerQuestion(tokens[1], tokens[2]));
                        break;
                    case Question.QuestionType.FillTheBlank:
                        QuestionBank.questions.Add(
                            new FillTheBlankQuestion(tokens[1],
                                                     int.Parse(tokens[2])));
                        break;
                    case Question.QuestionType.TrueFalse:
                        QuestionBank.questions.Add(
                            new TrueFalseQuestion(tokens[1], 
                                                  tokens[2]=="1"?true:false));
                        break;
                    case Question.QuestionType.MutliChoice:
                        QuestionBank.questions.Add(
                            new MutliChoiceQuestion(tokens[1], 
                                                    int.Parse(tokens[2]),
                                                    getChoices(tokens)));
                        break;
                    default:
                        break;
                }               
            }
        }

        List<string> getChoices(string[] lineTokens)
        {
            List<string> choices = new List<string>();
            for (int i = 3; i < lineTokens.Length; i++)
            {
                choices.Add(lineTokens[i]);
            }
            return choices;
        }
    }
}
