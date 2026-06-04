using Android;
using Android.Content;
using Android.Speech;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Microsoft.Extensions.Logging;

namespace VoiceToText.Services
{
    public partial class SpeechRecognitionService
    {
    private SpeechRecognizer? _speechRecognizer;
    private Intent? _speechRecognizerIntent;
    private readonly SpeechRecognitionListener _listener;

    partial void InitializePlatform()
    {
        _listener = new SpeechRecognitionListener(this);
    }

    public override async Task<bool> RequestPermissionsAsync()
    {
        var context = Platform.CurrentActivity ?? Android.App.Application.Context;
        
        if (ContextCompat.CheckSelfPermission(context, Manifest.Permission.RecordAudio) != Android.Content.PM.Permission.Granted)
        {
            if (Platform.CurrentActivity is AndroidX.Activity.ComponentActivity activity)
            {
                ActivityCompat.RequestPermissions(activity, new[] { Manifest.Permission.RecordAudio }, 1);
                
                // Wait a bit for permission dialog
                await Task.Delay(1000);
                
                return ContextCompat.CheckSelfPermission(context, Manifest.Permission.RecordAudio) == Android.Content.PM.Permission.Granted;
            }
            return false;
        }
        
        return true;
    }

    protected override async Task<bool> StartPlatformListeningAsync()
    {
        try
        {
            var context = Platform.CurrentActivity ?? Android.App.Application.Context;
            
            if (!await RequestPermissionsAsync())
            {
                OnErrorOccurred("Microphone permission not granted");
                return false;
            }

            _speechRecognizer = SpeechRecognizer.CreateSpeechRecognizer(context);
            _speechRecognizer.SetRecognitionListener(_listener);

            _speechRecognizerIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            _speechRecognizerIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
            _speechRecognizerIntent.PutExtra(RecognizerIntent.ExtraLanguage, "en-US");
            _speechRecognizerIntent.PutExtra(RecognizerIntent.ExtraPartialResults, true);

            _speechRecognizer.StartListening(_speechRecognizerIntent);
            return true;
        }
        catch (Exception ex)
        {
            OnErrorOccurred($"Android speech recognition error: {ex.Message}");
            return false;
        }
    }

    protected override async Task StopPlatformListeningAsync()
    {
        try
        {
            _speechRecognizer?.StopListening();
            _speechRecognizer?.Destroy();
            _speechRecognizer = null;
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            OnErrorOccurred($"Error stopping Android speech recognition: {ex.Message}");
        }
    }

    private class SpeechRecognitionListener : Java.Lang.Object, IRecognitionListener
    {
        private readonly SpeechRecognitionService _service;

        public SpeechRecognitionListener(SpeechRecognitionService service)
        {
            _service = service;
        }

        public void OnBeginningOfSpeech() { }
        public void OnBufferReceived(byte[] buffer) { }
        public void OnEndOfSpeech() { }
        public void OnEvent(int eventType, Bundle? @params) { }
        public void OnReadyForSpeech(Bundle? @params) { }
        public void OnRmsChanged(float rmsdB) { }

        public void OnError(SpeechRecognizerError error)
        {
            _service.OnErrorOccurred($"Speech recognition error: {error}");
            _service._isListening = false;
        }

        public void OnPartialResults(Bundle? partialResults)
        {
            var matches = partialResults?.GetStringArrayList(SpeechRecognizer.ResultsRecognition);
            if (matches?.Count > 0)
            {
                _service.OnSpeechRecognized(matches[0] ?? "");
            }
        }

        public void OnResults(Bundle? results)
        {
            var matches = results?.GetStringArrayList(SpeechRecognizer.ResultsRecognition);
            if (matches?.Count > 0)
            {
                _service.OnSpeechRecognized(matches[0] ?? "");
            }
            _service._isListening = false;
        }
    }
}
}
