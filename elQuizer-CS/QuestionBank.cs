﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elQuizer_CS
{
    class QuestionBank
    {
        public static List<Question> questions = new List<Question>();
        public static List<Question> shuffledQuestions = new List<Question>();

        public static void shuffleQuestions()
        {
            shuffledQuestions.Clear();
            int count = questions.Count;
            bool[] isVis = new bool[count];
            Random rand = new Random();
            for (int i = 0, t = rand.Next(count); i < count; ++i)
            {
                while (isVis[t])
                {
                    t = rand.Next(count);
                }
                isVis[t] = true;
                shuffledQuestions.Add(questions[t]);
            }
        }
        public static List<Question> parseQuestions(string[] lines)
        {
            List<Question> questionList = new List<Question>();
            foreach (var line in lines)
            {
                string[] tokens = line.Split(',');
                int firstChar = line[0] - '0';
                Question.QuestionType quesionType;
                quesionType = (Question.QuestionType)firstChar;
                switch (quesionType)
                {
                    case Question.QuestionType.ShortAnswer:
                        questionList.Add(
                            new ShortAnswerQuestion(tokens[1], tokens[2]));
                        break;
                    case Question.QuestionType.FillTheBlank:
                        questionList.Add(
                            new FillTheBlankQuestion(tokens[1],
                                                     int.Parse(tokens[2])));
                        break;
                    case Question.QuestionType.TrueFalse:
                        questionList.Add(
                            new TrueFalseQuestion(tokens[1],
                                                  tokens[2] == "1" ? true : false));
                        break;
                    case Question.QuestionType.MutliChoice:
                        questionList.Add(
                            new MutliChoiceQuestion(tokens[1],
                                                    int.Parse(tokens[2]),
                                                    getChoices(tokens)));
                        break;
                    default:
                        // File is corrupted.
                        return null;
                }
            }
            shuffleQuestions();
            return questionList;
        }
        private static List<string> getChoices(string[] lineTokens)
        {
            List<string> choices = new List<string>();
            for (int i = 3; i < lineTokens.Length; i++)
            {
                choices.Add(lineTokens[i]);
            }
            return choices;
        }
        public static List<string> getQuestionFileLines() {
            List<string> lines = new List<string>();
            foreach (var item in questions)
            {
                lines.Add(item.getFileLineString());
            }
            return lines;
        }
    }
}
