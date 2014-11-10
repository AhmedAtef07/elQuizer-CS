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
    /// Interaction logic for QuestionsManager.xaml
    /// </summary>
    public partial class QuestionsManager : Window
    {
        public QuestionsManager()
        {
            InitializeComponent();
            fillQuestionList();
            
        }

        void fillQuestionList()
        {
            foreach (var item in QuestionBank.questions)
            {
                string s = "";
                s += item.ToString() + '\n';
                s += item.getQuestion() + '\n';
                s += item.getAnswer().ToString();
                question_list.Items.Add(s);
            }
             
        }
    }
}
