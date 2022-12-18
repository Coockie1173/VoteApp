using Moq.Protected;
using Moq.Contrib.HttpClient;
using Moq;

namespace MAUITester
{
    public class UnitTest1
    {
        [Fact]
        public void TestValidSelection()
        {
            //setup mock HTTP client
            //var Disp = new Dispatcher();
            var handler = new Mock<HttpMessageHandler>();
            var client = handler.CreateClient();
            handler.SetupAnyRequest() //return OK with any request
                .ReturnsResponse(System.Net.HttpStatusCode.OK);

            VoteApp.AnswerQuestions AQ = new VoteApp.AnswerQuestions(client, false, false); //generate new answer questions page without grabbing questions
            VoteApp.Question Q = new VoteApp.Question();
            Q.questionid = 0;
            Q.SelectedOptions = 1;

            AQ.questions = new System.Collections.ObjectModel.ObservableCollection<VoteApp.Question>(); //generate an observable
            AQ.questions.Add(Q); //add Q
            AQ.QuestionDict = new Dictionary<int, VoteApp.Question>();
            AQ.QuestionDict.Add(Q.questionid, Q);

            Microsoft.Maui.Controls.Button B = new Microsoft.Maui.Controls.Button(); //generate a button to pass with button clicked
            B.BindingContext = Q; //add binding context
            EventArgs e = new ClickedEventArgs(ButtonsMask.Primary, true); //setup event args eventho I probably don't need em

            AQ.Button_Clicked(B, e);

            Assert.False(AQ.QuestionDict.ContainsKey(Q.questionid));
        }

        [Fact]
        public void TestInValidSelection()
        {
            //no need for a mock http client here since we never should reach the client code.
            VoteApp.AnswerQuestions AQ = new VoteApp.AnswerQuestions(null, false, false); //generate new answer questions page without grabbing questions
            VoteApp.Question Q = new VoteApp.Question();
            Q.questionid = 0;
            Q.SelectedOptions = -1;

            AQ.questions = new System.Collections.ObjectModel.ObservableCollection<VoteApp.Question>(); //generate an observable
            AQ.questions.Add(Q); //add Q
            AQ.QuestionDict = new Dictionary<int, VoteApp.Question>();
            AQ.QuestionDict.Add(Q.questionid, Q);

            Microsoft.Maui.Controls.Button B = new Microsoft.Maui.Controls.Button(); //generate a button to pass with button clicked
            B.BindingContext = Q; //add binding context
            EventArgs e = new ClickedEventArgs(ButtonsMask.Primary, true); //setup event args eventho I probably don't need em

            AQ.Button_Clicked(B, e);

            Assert.False(AQ.QuestionDict.ContainsKey(Q.questionid));
        }
    }
}