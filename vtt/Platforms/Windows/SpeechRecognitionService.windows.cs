using Microsoft.Extensions.Logging;
using Windows.Media.SpeechRecognition;
using Windows.Globalization;

namespace VoiceToText.Services
{
    public partial class SpeechRecognitionService
    {
    private SpeechRecognizer? _speechRecognizer;

    protected override async Task<bool> StartPlatformListeningAsync()
    {
        try
        {
            if (_speechRecognizer == null)
            {
                _speechRecognizer = new SpeechRecognizer(new Language("en-US"));
                
                // Set up continuous recognition
                _speechRecognizer.Constraints.Add(new SpeechRecognitionTopicConstraint(
                    SpeechRecognitionScenario.Dictation, "dictation"));
                
                await _speechRecognizer.CompileConstraintsAsync();
                
                _speechRecognizer.ContinuousRecognitionSession.ResultGenerated += OnResultGenerated;
                _speechRecognizer.ContinuousRecognitionSession.Completed += OnCompleted;
            }

            await _speechRecognizer.ContinuousRecognitionSession.StartAsync();
            return true;
        }
        catch (Exception ex)
        {
            OnErrorOccurred($"Windows speech recognition error: {ex.Message}");
            return false;
        }
    }

    protected override async Task StopPlatformListeningAsync()
    {
        try
        {
            if (_speechRecognizer?.ContinuousRecognitionSession != null)
            {
                await _speechRecognizer.ContinuousRecognitionSession.StopAsync();
            }
        }
        catch (Exception ex)
        {
            OnErrorOccurred($"Error stopping Windows speech recognition: {ex.Message}");
        }
    }

    private void OnResultGenerated(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionResultGeneratedEventArgs args)
    {
        if (args.Result.Confidence == SpeechRecognitionConfidence.Medium ||
            args.Result.Confidence == SpeechRecognitionConfidence.High)
        {
            OnSpeechRecognized(args.Result.Text);
        }
    }

    private void OnCompleted(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionCompletedEventArgs args)
    {
        _isListening = false;
        if (args.Status != SpeechRecognitionResultStatus.Success)
        {
            OnErrorOccurred($"Speech recognition completed with status: {args.Status}");
        }
    }
}
}
