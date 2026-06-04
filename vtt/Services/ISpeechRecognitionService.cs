namespace VoiceToText.Services
{
    public interface ISpeechRecognitionService
    {
        Task<bool> RequestPermissionsAsync();
        Task<bool> StartListeningAsync();
        Task StopListeningAsync();
        bool IsListening { get; }
        event EventHandler<string> SpeechRecognized;
        event EventHandler<string> ErrorOccurred;
    }
}
