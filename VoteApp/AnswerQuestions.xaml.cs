using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows.Input;

namespace VoteApp;

public partial class AnswerQuestions : ContentPage
{
    ObservableCollection<Question> questions;
    bool NotQuestionSelected = true;
    bool QuestionSelected = false;

    Dictionary<int, Question> QuestionDict = new Dictionary<int, Question>();

    public AnswerQuestions()
	{
		InitializeComponent();

        string url = GlobalData.URI + "/QuestionController/GetUnanswered/" + GlobalData.MyID;

        WebClient client = new WebClient(); //can't use async here :/

        // Send a GET request to the specified URL and store the response
        string responseBody = client.DownloadString(url);

        // Deserialize the response string into an array of Question objects
        questions = new ObservableCollection<Question>(JsonConvert.DeserializeObject<Question[]>(responseBody));

        UnansweredQuestionsListview.ItemsSource = questions;

        for(int i = 0; i < questions.Count; i++)
        {
            QuestionDict.Add(questions[i].questionid, questions[i]);
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        Button b = (Button)sender;
        Question Q = (Question)b.BindingContext;
        if(Q.SelectedOptions != -1)
        {
            QuestionDict.Remove(Q.questionid);
            questions.Remove(Q);

            using (HttpClient wc = new HttpClient())
            {
                var Parameters = new Dictionary<string, string> { { "UID", GlobalData.MyID }, { "QuestionID", Q.questionid.ToString() }, { "AnswerNumber", Q.SelectedOptions.ToString() } };
                var encodedContent = new FormUrlEncodedContent(Parameters);

                string url = GlobalData.URI + "/QuestionController/AnswerQuestion/" + GlobalData.MyID + "/" + Q.questionid + "/" + Q.SelectedOptions;

                var response = await wc.PostAsync(url, encodedContent);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    // Do something with response. Example get content:
                    // var responseContent = await response.Content.ReadAsStringAsync ().ConfigureAwait (false);
                    Environment.Exit(-1);
                }
            }
        }
    }

    private void AnswerPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker p = (Picker)sender;
        Question Q = (Question)p.BindingContext;
        Q.SelectedOptions = p.SelectedIndex;
    }
}