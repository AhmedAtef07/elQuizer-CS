﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace elQuizer_CS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Started the project @2:10 pm 10/11/2014
    /// Ahmed Atef
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            QuestionBank.questions = QuestionBank.parseQuestions(Elfile.load(@"C:\Users\Ahmed\Documents\test02.qbank"));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddNewQuestion addNewQuestion = new AddNewQuestion();
            //addNewQuestion.Show();
            addNewQuestion.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            QuestionsManager questionsManager = new QuestionsManager();
            questionsManager.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Practice practice = new Practice();
            practice.ShowDialog();
        }
    }
}
