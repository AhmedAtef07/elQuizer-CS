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
            : base(question, answer, QuestionType.TrueFalse, AnswerType.Choice) { }
        public override string getFileLineString()
        {
            return getQuestionTypeValue() + "," + base.getQuestion() + ","
                + ((bool)getAnswer()?"1":"0");
        }
        public override string getMessage()
        {
            return "It is either true or false.";
        }

        public List<String> getChoices()
        {
            List<String> choices = new List<String>();
            choices.Add("True");
            choices.Add("False");
            return choices;
        }
    }
}
