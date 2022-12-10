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

        WebClient client = new WebClient(); //no async, async bad

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

    private void Button_Clicked(object sender, EventArgs e)
    {
        Button b = (Button)sender;
        Question Q = (Question)b.BindingContext;
        if(Q.SelectedOptions != -1)
        {
            QuestionDict.Remove(Q.questionid);
            questions.Remove(Q);
        }
    }

    private void AnswerPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker p = (Picker)sender;
        Question Q = (Question)p.BindingContext;
        Q.SelectedOptions = p.SelectedIndex;
    }
}