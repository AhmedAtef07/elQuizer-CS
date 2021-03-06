﻿using Microsoft.Win32;
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
    public partial class QuestionManager : Window
    {
        public QuestionManager()
        {
            InitializeComponent();
            ElFile.getLocalPaths();
            fillQuestionList();
            fillPathList();
            ElFile.getLastAccessedFile();
            selectListItem(ElFile.currentFile);
        }

        private void fillPathList()
        {
            ElFile.getLocalPaths();
            dirs_list.Items.Clear();
            foreach (string path in ElFile.savedPaths)
            {
                string fileName = ElFile.extractFileName(path);
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
        
        void selectListItem(int index)
        {
            if (dirs_list.Items.Count > 0)
            {
                dirs_list.SelectedIndex = index;
            }
        }

        void selectListItem(string filename)
        {
            for (int i = 0; i < ElFile.savedPaths.Count; i++)
            {
                if (ElFile.extractFileName(ElFile.savedPaths[i]) == filename)
                {
                    dirs_list.SelectedIndex = i;
                    return;
                }
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
            ElFile.updateCurrentFile();
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
            Label label = (Label)dirs_list.SelectedItem;
            if (label == null)
            {
                return;
            }
            ElFile.delete(label.Content.ToString());
            fillPathList();
        }

        private void add_new_btn(object sender, RoutedEventArgs e)
        {

            AddNewQuestion addNewQuestion = new AddNewQuestion();
            addNewQuestion.ShowDialog();
            fillQuestionList();
        }

        private void dirs_list_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                delete_btn_click(null, null);
            }
        }

        private void question_list_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (question_list.SelectedIndex == -1)
                {
                    return;
                }
                ElTools.removeAt(question_list.SelectedIndex);
                fillQuestionList();
            }
        }
        
    }
}
