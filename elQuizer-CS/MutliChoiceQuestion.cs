using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elQuizer_CS
{
    class MutliChoiceQuestion : Question
    {
        List<String> choices;
        public MutliChoiceQuestion(string question, int answer) 
            : base(question, answer, QuestionType.MutliChoice) {
                choices = new List<String>();                
        }

        public override bool checkAnswer(string answer)
        {
            foreach (string item in choices)
            {
                if (item == answer) {
                    
                }
            }
            return base.checkAnswer(answer);
        }
    }
}
