using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net;

namespace VoteApp;

public partial class AnsweredQuestionsPage : ContentPage
{
	public AnsweredQuestionsPage()
	{
		InitializeComponent();

        string url = GlobalData.URI + "/QuestionController/GetAnswered/";

        WebClient client = new WebClient(); //can't use async here :/

        // Send a GET request to the specified URL and store the response
        string responseBody = client.DownloadString(url);

        AnsweredQuestion AQ;

        // Deserialize the response string into an array of Question objects
        AQ = JsonConvert.DeserializeObject<AnsweredQuestion>(responseBody);
    }
}