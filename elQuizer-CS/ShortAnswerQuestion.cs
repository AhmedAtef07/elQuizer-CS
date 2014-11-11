using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elQuizer_CS
{
    class ShortAnswerQuestion : Question
    {
        public ShortAnswerQuestion(string question, string answer)
            : base(question, answer, QuestionType.ShortAnswer, AnswerType.Text) 
        { }
        public override string getMessage()
        {
            return "Answer the question.";
        }
    }
}
