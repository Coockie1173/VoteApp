namespace VoteApp;

public partial class App : Application
{
	public App(IGetDeviceInfo getDeviceInfo)
	{
		InitializeComponent();

		GlobalData.MyID = getDeviceInfo.GetDeviceID();

		MainPage = new AppShell();
	}
}
