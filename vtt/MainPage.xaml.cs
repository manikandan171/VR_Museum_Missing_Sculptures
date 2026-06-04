using VoiceToText.Services;
using Microsoft.Extensions.Logging;

namespace VoiceToText
{
    public partial class MainPage : ContentPage
    {
        private readonly ISpeechRecognitionService _speechService;
        private readonly ILogger<MainPage> _logger;
        private string _recognizedText = "";

        public MainPage(ISpeechRecognitionService speechService, ILogger<MainPage> logger)
        {
        InitializeComponent();
        _speechService = speechService;
        _logger = logger;

        // Subscribe to speech recognition events
        _speechService.SpeechRecognized += OnSpeechRecognized;
        _speechService.ErrorOccurred += OnErrorOccurred;
    }

    private async void OnRecordButtonClicked(object sender, EventArgs e)
    {
        try
        {
            if (_speechService.IsListening)
            {
                await StopRecording();
            }
            else
            {
                await StartRecording();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling record button click");
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    private async Task StartRecording()
    {
        try
        {
            StatusLabel.Text = "Requesting permissions...";
            
            var hasPermission = await _speechService.RequestPermissionsAsync();
            if (!hasPermission)
            {
                await DisplayAlert("Permission Required", 
                    "Microphone permission is required for speech recognition.", "OK");
                StatusLabel.Text = "Permission denied";
                return;
            }

            StatusLabel.Text = "Starting recording...";
            var started = await _speechService.StartListeningAsync();
            
            if (started)
            {
                RecordButton.Text = "🛑 Stop Recording";
                RecordButton.BackgroundColor = Colors.Red;
                StatusLabel.Text = "Listening... Speak now!";
            }
            else
            {
                StatusLabel.Text = "Failed to start recording";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error starting recording");
            StatusLabel.Text = "Error starting recording";
            await DisplayAlert("Error", $"Failed to start recording: {ex.Message}", "OK");
        }
    }

    private async Task StopRecording()
    {
        try
        {
            StatusLabel.Text = "Stopping recording...";
            await _speechService.StopListeningAsync();
            
            RecordButton.Text = "🎤 Start Recording";
            RecordButton.BackgroundColor = Color.FromArgb("#512BD4"); // Primary color
            StatusLabel.Text = "Recording stopped";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error stopping recording");
            StatusLabel.Text = "Error stopping recording";
            await DisplayAlert("Error", $"Failed to stop recording: {ex.Message}", "OK");
        }
    }

    private void OnSpeechRecognized(object? sender, string recognizedText)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (!string.IsNullOrWhiteSpace(recognizedText))
            {
                _recognizedText += recognizedText + " ";
                RecognizedTextLabel.Text = _recognizedText.Trim();
                StatusLabel.Text = "Speech recognized!";
            }
        });
    }

    private void OnErrorOccurred(object? sender, string error)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            StatusLabel.Text = "Error occurred";
            RecordButton.Text = "🎤 Start Recording";
            RecordButton.BackgroundColor = Color.FromArgb("#512BD4"); // Primary color
            
            await DisplayAlert("Speech Recognition Error", error, "OK");
        });
    }

    private void OnClearButtonClicked(object sender, EventArgs e)
    {
        _recognizedText = "";
        RecognizedTextLabel.Text = "Your speech will appear here...";
        StatusLabel.Text = "Text cleared";
    }

    private async void OnCopyButtonClicked(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(_recognizedText))
            {
                await Clipboard.SetTextAsync(_recognizedText.Trim());
                StatusLabel.Text = "Text copied to clipboard!";
                
                // Reset status after 2 seconds
                await Task.Delay(2000);
                if (StatusLabel.Text == "Text copied to clipboard!")
                {
                    StatusLabel.Text = "Ready to listen";
                }
            }
            else
            {
                await DisplayAlert("No Text", "There is no text to copy.", "OK");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error copying text to clipboard");
            await DisplayAlert("Error", "Failed to copy text to clipboard.", "OK");
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        
        // Clean up - stop listening if still active
        if (_speechService.IsListening)
        {
            Task.Run(async () => await _speechService.StopListeningAsync());
        }
    }
}
}
