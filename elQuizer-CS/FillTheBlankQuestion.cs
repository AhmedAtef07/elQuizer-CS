﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace elQuizer_CS
{
    class FillTheBlankQuestion : Question
    {
        private string[] questionTokens;
        private int answerIndex;
        public FillTheBlankQuestion(string question, int answer)
            : base(question, null, QuestionType.FillTheBlank, AnswerType.Text) 
        {
            question = question.Trim();
            this.questionTokens = Regex.Split(question, @"\s+");
            setAnswer(questionTokens[answer]);
            this.answerIndex = answer;
        }

        public override string getQuestion()
        {
            string question = "";
            for (int i = 0; i < questionTokens.Length; ++i)
            {
                if (i == answerIndex)
	            {
		            question += "_______ ";
                    continue;
	            }
                question += questionTokens[i] + ' ';
            }
            return question;
        }

        public override string getFileLineString()
        {
            return (int)getQuestionType() + Delimiter.ToString() +
                   base.getQuestion() + Delimiter.ToString() +
                   answerIndex;
        }

        public override string getMessage()
        {
            return "Fill the blank.";
        }
    
    }
}
