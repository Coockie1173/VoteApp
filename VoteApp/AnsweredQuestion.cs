using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteApp
{
    internal class AnsweredQuestion
    {
        public Dictionary<int, int> QA = new Dictionary<int, int>();
        public string[] Options = new string[0];
        public string Question { get; set; }

        //disgusting :/
        public RowDefinitionCollection OptionsAmm { get 
            {
                RowDefinitionCollection rows = new RowDefinitionCollection();
                for(int i = 0; i < Options.Length; i++)
                {
                    rows.Add(new RowDefinition());
                }
                return rows;
            } 
        }

        public string[] GetAnswerOptions()
        {
            List<string> options = new List<string>();
            foreach(int key in QA.Keys)
            {
                if(key < Options.Count()) //valid key
                {
                    options.Add("Answer " + Options[key] + " has been chosen " + QA[key]);
                }
            }
            return options.ToArray();
        }
    }
}
