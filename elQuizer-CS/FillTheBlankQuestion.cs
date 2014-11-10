using System;
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
            : base(question, null, QuestionType.FillTheBlank) 
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
		            question += "_______";
	            }
                question += questionTokens[i];
            }
            return question;
        }
    
    }
}
