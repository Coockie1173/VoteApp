using Microsoft.Extensions.Logging;
using VoteApp.Platforms;
using VoteApp.Platforms.DeviceStuff;

namespace VoteApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
            .Services.AddTransient<IGetDeviceInfo, GetDeviceInfo>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
