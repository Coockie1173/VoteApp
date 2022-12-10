using System.Net;

namespace VoteApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AnswerQuestions());
    }

    private async void VerticalStackLayout_Loaded(object sender, EventArgs e)
    {
        using (HttpClient wc = new HttpClient())
        {
            var Parameters = new Dictionary<string, string> { { "UID", GlobalData.MyID } };
            var encodedContent = new FormUrlEncodedContent(Parameters);

            string url = GlobalData.URI + "/QuestionController/informDB/" + GlobalData.MyID;

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

