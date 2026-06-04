using Microsoft.Extensions.Logging;

namespace VoiceToText.Services
{
    public partial class SpeechRecognitionService : ISpeechRecognitionService
    {
    private readonly ILogger<SpeechRecognitionService> _logger;
    private bool _isListening;

    public SpeechRecognitionService(ILogger<SpeechRecognitionService> logger)
    {
        _logger = logger;
        InitializePlatform();
    }

    partial void InitializePlatform() { }

    public bool IsListening => _isListening;

    public event EventHandler<string>? SpeechRecognized;
    public event EventHandler<string>? ErrorOccurred;

    public virtual async Task<bool> RequestPermissionsAsync()
    {
        // Base implementation - platform-specific implementations will override
        await Task.CompletedTask;
        return true;
    }

    public virtual async Task<bool> StartListeningAsync()
    {
        try
        {
            _isListening = true;
            _logger.LogInformation("Speech recognition started");
            return await StartPlatformListeningAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error starting speech recognition");
            ErrorOccurred?.Invoke(this, ex.Message);
            _isListening = false;
            return false;
        }
    }

    public virtual async Task StopListeningAsync()
    {
        try
        {
            _isListening = false;
            await StopPlatformListeningAsync();
            _logger.LogInformation("Speech recognition stopped");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error stopping speech recognition");
            ErrorOccurred?.Invoke(this, ex.Message);
        }
    }

    protected virtual async Task<bool> StartPlatformListeningAsync()
    {
        // Default implementation for unsupported platforms
        await Task.Delay(100);
        ErrorOccurred?.Invoke(this, "Speech recognition not supported on this platform");
        return false;
    }

    protected virtual async Task StopPlatformListeningAsync()
    {
        await Task.CompletedTask;
    }

    protected void OnSpeechRecognized(string text)
    {
        _logger.LogInformation($"Speech recognized: {text}");
        SpeechRecognized?.Invoke(this, text);
    }

    protected void OnErrorOccurred(string error)
    {
        _logger.LogError($"Speech recognition error: {error}");
        ErrorOccurred?.Invoke(this, error);
    }
}
}
