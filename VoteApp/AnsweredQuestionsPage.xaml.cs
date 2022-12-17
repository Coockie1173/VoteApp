using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net;

namespace VoteApp;

public partial class AnsweredQuestionsPage : ContentPage
{
    Dictionary<string, AnsweredQuestion> AQD;

    public AnsweredQuestionsPage()
	{
		InitializeComponent();

        string url = GlobalData.URI + "/QuestionController/GetAnswered/";

        WebClient client = new WebClient(); //can't use async here :/

        // Send a GET request to the specified URL and store the response
        string responseBody = client.DownloadString(url);

        AnsweredQuestion[] AQ;
        AQD = new Dictionary<string, AnsweredQuestion>();

        // Deserialize the response string into an array of Question objects
        AQ = JsonConvert.DeserializeObject<AnsweredQuestion[]>(responseBody);
        AnsweredQuestions.ItemsSource = AQ;

        foreach(AnsweredQuestion aq in AQ)
        {
            AQD.Add(aq.Question, aq); //add for easy access later
        }
    }

    private void Grid_Loaded(object sender, EventArgs e)
    {
        Grid obj = (Grid)sender;
        //when loaded, add in the epic labels in the correct rows n stuff

        AnsweredQuestion aq = (AnsweredQuestion)obj.BindingContext;

        obj.Add(new Label
        {
            Text = aq.Question,
            FontSize = 14,
            Margin = new Microsoft.Maui.Thickness(20, 0, 0, 0),
            HorizontalTextAlignment = Microsoft.Maui.TextAlignment.Start,
            HorizontalOptions = LayoutOptions.Start,
        }, 0, 0);

        int row = 0;
        for (int i = 0; i < aq.Options.Length; i++)
        {
            obj.Add(new Label
            {
                Text = aq.Options[i],
                FontSize = 14,
                HorizontalTextAlignment = Microsoft.Maui.TextAlignment.End,
                HorizontalOptions = LayoutOptions.End
            }, 1, row);

            if(aq.QA.ContainsKey(i))
            {
                obj.Add(new Label
                {
                    Text = aq.QA[i].ToString(),
                    FontSize = 14,
                    Margin = new Microsoft.Maui.Thickness(20, 0, 0, 0),
                    HorizontalTextAlignment = Microsoft.Maui.TextAlignment.End,
                    HorizontalOptions = LayoutOptions.End
                }, 2, row);
            }
            else
            {
                obj.Add(new Label
                {
                    Text = "0",
                    FontSize = 14,
                    Margin = new Microsoft.Maui.Thickness(20, 0, 0, 0),
                    HorizontalTextAlignment = Microsoft.Maui.TextAlignment.End,
                    HorizontalOptions = LayoutOptions.End
                }, 2, row);
            }


            row++;
        }
    }
}