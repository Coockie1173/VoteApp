using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteApp
{
    public class Question
    {
        public int questionid { get; set; }

        public string _question { get; set; }

        public string[] options { get; set; }

        public int SelectedOptions = -1;
    }
}
