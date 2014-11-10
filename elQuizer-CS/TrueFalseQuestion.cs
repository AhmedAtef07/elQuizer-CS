using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elQuizer_CS
{
    class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion(string question, bool answer) 
            : base(question, answer, QuestionType.TrueFalse) { }
    }
}
