using Microsoft.Extensions.Logging;
using VoiceToText.Services;

namespace VoiceToText
{
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
            });

#if DEBUG
        builder.Services.AddLogging(logging =>
        {
            logging.AddDebug();
        });
#endif

        // Register speech recognition service
        builder.Services.AddSingleton<ISpeechRecognitionService, SpeechRecognitionService>();
        
        // Register MainPage
        builder.Services.AddTransient<MainPage>();

        return builder.Build();
    }
}
}
