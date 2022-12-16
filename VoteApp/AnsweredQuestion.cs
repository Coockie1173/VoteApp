using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteApp
{
    internal class AnsweredQuestion
    {
        public Dictionary<string, Dictionary<int, int>> QA = new Dictionary<string, Dictionary<int, int>>();
        public Dictionary<string, string[]> Options = new Dictionary<string, string[]>();



        public string[] GetQuestions()
        {
            return Options.Keys.ToArray(); //get all questions
        }
    }
}
