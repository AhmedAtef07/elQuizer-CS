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
    public partial class QuestionsManager : Window
    {
        string[] lines;
        public QuestionsManager()
        {
            InitializeComponent();
            fillQuestionList();
        }
        void fillQuestionList()
        {            
            foreach (var question in QuestionBank.questions)
            {
                //string s = "";
                //s += item.ToString() + '\n';
                //s += item.getQuestion() + '\n';
                //s += item.getAnswer().ToString();
                //question_list.Items.Add(s);

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
                    string.Join("\n",lines.ToArray()));
                MessageBox.Show("File Saved."); 
            }
        }  

        private void load_btn_click(object sender, RoutedEventArgs e)
        {
            lines = Elfile.load(@"C:\Users\Ahmed\Documents\test02.qbank");
            if (lines == null)
            {
                MessageBox.Show("Unable to locate the file.");
                lines = Elfile.load();
            }
            QuestionBank.questions = QuestionBank.parseQuestions(lines);
            MessageBox.Show("File loaded.");
            fillQuestionList();
            
        }
        
    }
}
