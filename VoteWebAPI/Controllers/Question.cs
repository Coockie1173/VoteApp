namespace VoteWebAPI.Controllers
{
    public class Question
    {
        public int questionid { get; set; }

        public string _question { get; set; }

        public string[] options { get; set; }

        public int SelectedOptions = -1;

        public Question()
        {
            _question = "";
            options = new string[0];
        }

        public override string ToString()
        {
            return questionid.ToString() + " " + _question;
        }
    }
}