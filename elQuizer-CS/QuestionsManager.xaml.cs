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
            Elfile.getLocalPaths();
            fillQuestionList();
            fillPathList();
            selectListItem(0);
        }

        private void fillPathList()
        {
            Elfile.getLocalPaths();
            dirs_list.Items.Clear();
            foreach (string path in Elfile.savedPaths)
            {
                string[] token = path.Split('\\');
                string fileName = token[token.Length - 1];
                string[] fileNameTokens = fileName.Split('.');
                if (fileNameTokens[fileNameTokens.Length - 1] != "qbank" ||
                    fileNameTokens.Length == 1)
                {
                    continue;
                }
                Label fileName_lbl = new Label();
                fileName_lbl.Content = fileName.Replace(".qbank", "");
                fileName_lbl.Tag = path;
                dirs_list.Items.Add(fileName_lbl);
            }            
        }
        void selectListItem(int index)
        {
            if (dirs_list.Items.Count > 0)
            {
                dirs_list.SelectedIndex = index;
            }
        }
        void fillQuestionList()
        {
            question_list.Items.Clear();
            foreach (var question in QuestionBank.questions)
            {
                question_list.Items.Add(question.getFileLineString());
            }
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
                    string.Join(
                    "\n", QuestionBank.getQuestionFileLines().ToArray()));
                MessageBox.Show("File Saved.");
                fillPathList();
            }
        }  

        private void load_btn_click(object sender, RoutedEventArgs e)
        {
            string[] lines = Elfile.load();
            if (lines != null)
            {
                QuestionBank.questions = QuestionBank.parseQuestions(lines);
                MessageBox.Show("File loaded.");
                fillQuestionList();
            }
            
        }

        private void clear_btn_click(object sender, RoutedEventArgs e)
        {
            QuestionBank.questions.Clear();
            fillQuestionList();
        }

        private void dirs_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            foreach (Label item in listBox.Items)
            {
                item.FontWeight = FontWeights.Normal;
            }
            Label selectedLabel = (Label)listBox.SelectedItem;
            selectedLabel.FontWeight = FontWeights.Bold;

            string[] newLines = Elfile.load(selectedLabel.Tag.ToString());
            if (newLines == null)
            {
                MessageBox.Show("File not found!");
                fillPathList();
                return;
            }
            QuestionBank.questions = QuestionBank.parseQuestions(newLines);
            fillQuestionList();

        }

        private void save_btn_click(object sender, RoutedEventArgs e)
        {
            
        }
        
    }
}
