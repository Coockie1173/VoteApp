using System.Collections.Generic;

namespace VoteWebAPI.Controllers
{
    public class AnsweredQuestion
    {
        public Dictionary<string, Dictionary<int, int>> QA = new Dictionary<string, Dictionary<int, int>>();
        public Dictionary<string, string[]> Options = new Dictionary<string, string[]>();
    }
}
