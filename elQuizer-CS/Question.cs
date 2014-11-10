using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elQuizer_CS
{
    abstract class Question
    {
        public enum QuestionType
        {
            ShortAnswer = 0,
            FillTheBlank = 1,
            TrueFalse = 2,
            MutliChoice = 3,
        }
        string question;
        object answer;
        QuestionType questionType;
        public Question(string question, object answer,
                        QuestionType questionType)
        {
            this.question = question;
            this.answer = answer;
            this.questionType = questionType;
        }

        public string getQuestion() {
            return question;
        }

        public object getAnswer() {
          return answer;
        }

        public virtual bool checkAnswer(object answer) {
            if (answer is String)
            {
                return (((string)this.answer).ToLower() ==
                       ((string)answer).ToLower());
            }
            return this.answer.Equals(answer) ;
        }    
    
        public string toString() {
            return "Question: " + question ;
        }
    }
}
