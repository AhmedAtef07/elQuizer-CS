using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace elQuizer_CS
{
    public class BaseWindow : Window
    {
        protected int questionsCount,
                      currQuestionIndex;
        protected Rectangle[] rects;
        protected TextBox textAnswer;
        protected StackPanel choices_sp;
        protected Question currQuestion;
        protected bool isFinished;

        // Window Controls.
        protected TextBlock question_txt, question_message_txt;
        protected Grid answer_grid, progress_grid;
        protected Label question_order_txt;
        protected Button action_btn;

        protected void MyWindow_Closing(object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            if (!isFinished)
            {
                MessageBoxResult result = MessageBox.Show(
                    "You really want to close?", "Escaping?",
                    MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        protected void showQuestion(int questionIndex)
        {
            currQuestion = ElTools.shuffledQuestions[currQuestionIndex];
            question_txt.Text = currQuestion.getQuestion();

            question_message_txt.Text = currQuestion.getMessage();

            answer_grid.Children.RemoveRange(1, answer_grid.Children.Count - 1);
            switch (currQuestion.getAnswerType())
            {
                case Question.AnswerType.Text:
                    question_order_txt.Content = "Answer:";

                    textAnswer = new TextBox();
                    textAnswer.VerticalAlignment = VerticalAlignment.Stretch;
                    textAnswer.MaxLength = 100;
                    textAnswer.Height = 27;
                    textAnswer.VerticalContentAlignment = (
                        VerticalAlignment.Center);
                    textAnswer.Margin = new Thickness(0, 0, 7, 0);
                    Grid.SetColumn(textAnswer, 1);
                    answer_grid.Children.Add(textAnswer);
                    textAnswer.Focus();
                    break;
                case Question.AnswerType.Choice:
                    question_order_txt.Content = "Choose:";

                    choices_sp = new StackPanel();
                    choices_sp.VerticalAlignment = VerticalAlignment.Center;
                    choices_sp.Margin = new Thickness(10, 10, 0, 0);
                    List<string> choices = new List<string>();
                    switch (currQuestion.getQuestionType())
                    {
                        case Question.QuestionType.TrueFalse:
                            choices = ((TrueFalseQuestion)
                                currQuestion).getChoices();
                            break;
                        case Question.QuestionType.MutliChoice:
                            choices = ((MutliChoiceQuestion)
                                currQuestion).getShuffledChoices();
                            break;
                        default:
                            break;
                    }
                    for (int i = 0; i < choices.Count; ++i)
                    {
                        RadioButton NewChoice = new RadioButton();
                        NewChoice.Margin = new Thickness(0, 0, 0, 7);
                        NewChoice.Content = choices[i];
                        choices_sp.Children.Add(NewChoice);
                    }

                    Grid.SetColumn(choices_sp, 1);
                    answer_grid.Children.Add(choices_sp);
                    choices_sp.Children[0].Focus();
                    break;
                default:
                    break;
            }


        }
        protected void prepareStage()
        {
            ElTools.shuffleQuestions();
            Report.userAnswers.Clear();
            Report.userBooleans.Clear();
            currQuestionIndex = 0;
            questionsCount = ElTools.shuffledQuestions.Count;
            action_btn.Tag = "0";
            isFinished = false;
            setProgressGrid();
            showQuestion(0);
        }

        protected void setProgressGrid()
        {
            // questionsCount = 30;
            rects = new Rectangle[questionsCount];

            for (int i = 0; i < questionsCount; ++i)
            {
                ColumnDefinition cd = new ColumnDefinition();
                progress_grid.ColumnDefinitions.Add(cd);

                rects[i] = new Rectangle();
                rects[i].Fill = new SolidColorBrush(Colors.LightGray);
                rects[i].Height = 17;
                if (i != 0) rects[i].Margin = new Thickness(3, 0, 0, 0);

                Grid.SetColumn(rects[i], i);
                progress_grid.Children.Add(rects[i]);
            }

        }

        protected RadioButton getCheckedRadioButton()
        {
            for (int i = 0; i < choices_sp.Children.Count; ++i)
            {
                if (((RadioButton)choices_sp.Children[i]).IsChecked == true)
                {
                    return (RadioButton)choices_sp.Children[i];
                }
            }
            return null;
        }
        protected RadioButton getCorrectAnswerRadioButton()
        {
            string answer = currQuestion.getAnswer().ToString().ToLower();
            for (int i = 0; i < choices_sp.Children.Count; ++i)
            {
                if (((RadioButton)choices_sp.Children[i]
                    ).Content.ToString().ToLower() == answer)
                {
                    return (RadioButton)choices_sp.Children[i];
                }
            }
            return null;
        }
        protected virtual void action_btn_Click(object sender, RoutedEventArgs e) {

        }
        protected void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return &&
                (Keyboard.Modifiers & ModifierKeys.Control) ==
                ModifierKeys.Control)
            {
                action_btn_Click(null, null);
            }
        }

        protected int checkAnswer()
        {
            string answer;
            switch (currQuestion.getAnswerType())
            {
                case Question.AnswerType.Text:
                    if (textAnswer.Text == "")
                    {
                        MessageBox.Show("Where is the answer?");
                        return -1;
                    }
                    answer = textAnswer.Text.Trim();
                    Report.userAnswers.Add(answer);
                    return currQuestion.checkAnswer(answer) ? 1 : 0;
                case Question.AnswerType.Choice:
                    switch (currQuestion.getQuestionType())
                    {
                        case Question.QuestionType.TrueFalse:
                            for (int i = 0; i < choices_sp.Children.Count; ++i)
                            {
                                if (((RadioButton)choices_sp.Children[i])
                                    .IsChecked == true)
                                {
                                    answer = ((RadioButton)choices_sp.
                                        Children[i]).Content.ToString();
                                    Report.userAnswers.Add(answer);
                                    return currQuestion.checkAnswer(answer
                                        == "True") ? 1 : 0;
                                }
                            }
                            MessageBox.Show("Pick a choice!");
                            return -1;
                        case Question.QuestionType.MutliChoice:
                            RadioButton selectedRB = getCheckedRadioButton();
                            if (selectedRB != null)
                            {
                                answer = selectedRB.Content.ToString();
                                Report.userAnswers.Add(answer);
                                return currQuestion.checkAnswer(answer) ? 1 : 0;
                            }
                            MessageBox.Show("Pick a choice!");
                            return -1;
                        default:
                            break;
                    }

                    break;
                default:
                    break;
            }
            return -1;
        }       
    }

}
