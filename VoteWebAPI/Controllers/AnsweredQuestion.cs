using System.Collections.Generic;

namespace VoteWebAPI.Controllers
{
    public class AnsweredQuestion
    {
        public Dictionary<int, int> QA = new Dictionary<int, int>();
        public string[] Options = new string[0];
        public string Question = "";
        //public Dictionary<string, Dictionary<int, int>> QA = new Dictionary<string, Dictionary<int, int>>();
        //public Dictionary<string, string[]> Options = new Dictionary<string, string[]>();
    }
}
