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
        public MutliChoiceQuestion(string question, List<String> choices, 
            int answer) : base(question, choices[answer], 
                               QuestionType.MutliChoice)
        {
            this.choices = choices;
        }

        public List<String> getChoices()
        {   
            return choices;
        }

        public List<String> getShuffledChoices()
        {
            bool[] isVis = new bool[choices.Count];
            List<String> shuffledChoices = new List<string>();
            Random rand = new Random();
            for (int i = 0, t = rand.Next(choices.Count); i < choices.Count; ++i) {
                while (!isVis[t]) {
                    t = rand.Next(choices.Count);
                }
                isVis[t] = true;
                shuffledChoices.Add(choices[t]);
            }
            return shuffledChoices;
        }

    }
}
