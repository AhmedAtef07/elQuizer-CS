﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elQuizer_CS
{
    public abstract class Question
    {
        public enum QuestionType
        {
            ShortAnswer = 0,
            FillTheBlank = 1,
            TrueFalse = 2,
            MutliChoice = 3,
        }
        public enum AnswerType
        {
            Text = 0,
            Choice = 1,
        }
        string question;
        object answer;
        QuestionType questionType;
        AnswerType answerType;

        static char delimiter = (char)7;

        public static char Delimiter
        {
            get { return delimiter; }
        }
        public Question(string question, object answer,
                        QuestionType questionType, AnswerType answerType)
        {
            this.question = question;
            this.answer = answer;
            this.questionType = questionType;
            this.answerType = answerType;
        }

        public virtual string getQuestion()
        {
            return question;
        }

        public object getAnswer()
        {
            return answer;
        }

        public QuestionType getQuestionType()
        {
            return questionType;
        }
        public AnswerType getAnswerType()
        {
            return answerType;
        }
       
        public void setAnswer(object answer)
        {
            this.answer = answer;
        }
        public bool checkAnswer(object answer)
        {
            if (answer is String)
            {
                return (((string)this.answer).ToLower() ==
                       ((string)answer).ToLower().Trim());
            }
            return this.answer.Equals(answer);
        }

        public override string ToString()
        {
            return "Type: " + questionType + "\n" +
                   "Question: " + getQuestion() + "\n" +
                   "Answer: " + getAnswer();
        }
        public virtual string getFileLineString()
        {
            return (int)getQuestionType() + Delimiter.ToString() +
                   getQuestion() + Delimiter.ToString() + 
                   getAnswer();
        }
        public virtual string getMessage()
        {
            throw new NotImplementedException();
        }
    }
}
