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
            ElFile.getLocalPaths();
            fillQuestionList();
            fillPathList();
            ElFile.getLastAccessedFile();
            selectListItem(0);
        }

        private void fillPathList()
        {
            ElFile.getLocalPaths();
            dirs_list.Items.Clear();
            foreach (string path in ElFile.savedPaths)
            {
                string fileName = cleanFileName(path);
                if (fileName == null)
                {
                    continue;
                }
                Label fileName_lbl = new Label();
                fileName_lbl.Content = fileName;
                fileName_lbl.Tag = path;
                dirs_list.Items.Add(fileName_lbl);
            }            
        }
        string cleanFileName(string fullPath)
        {
            string[] token = fullPath.Split('\\');
            string fileName = token[token.Length - 1];
            string[] fileNameTokens = fileName.Split('.');
            if (fileNameTokens[fileNameTokens.Length - 1] != "qbank" ||
                fileNameTokens.Length == 1)
            {
                return null;
            }
            return fileName.Replace(".qbank", "");
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
            foreach (var question in ElTools.questions)
            {
                question_list.Items.Add(question.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ElFile.export())
            {
                MessageBox.Show("File saved.");
                fillPathList();
            }
        }  

        private void load_btn_click(object sender, RoutedEventArgs e)
        {
            string[] lines = ElFile.load();
            if (lines != null)
            {
                ElTools.parseQuestions(lines);
                MessageBox.Show("File loaded.");
                fillQuestionList();
            }
            
        }

        private void clear_btn_click(object sender, RoutedEventArgs e)
        {
            ElTools.questions.Clear();
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
            if (selectedLabel == null)
            {
                return;
            }
            selectedLabel.FontWeight = FontWeights.Bold;

            string[] newLines = ElFile.load(selectedLabel.Tag.ToString());
            if (newLines == null)
            {
                MessageBox.Show("File disappered!");
                fillPathList();
                return;
            }
            ElTools.parseQuestions(newLines);
            fillQuestionList();

        }     

        private void duplicate_btn_click(object sender, RoutedEventArgs e)
        {
            ElFile.duplicate(((Label)dirs_list.SelectedItem).Content.ToString());
            fillPathList();
        }

        private void delete_btn_click(object sender, RoutedEventArgs e)
        {
            ElFile.delete(((Label)dirs_list.SelectedItem).Content.ToString());
            fillPathList();
        }
        
    }
}
