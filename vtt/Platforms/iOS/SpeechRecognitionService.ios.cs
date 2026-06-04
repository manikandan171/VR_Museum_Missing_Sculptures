using AVFoundation;
using Foundation;
using Speech;
using Microsoft.Extensions.Logging;

namespace VoiceToText.Services
{
    public partial class SpeechRecognitionService
    {
    private SFSpeechRecognizer? _speechRecognizer;
    private SFSpeechAudioBufferRecognitionRequest? _recognitionRequest;
    private SFSpeechRecognitionTask? _recognitionTask;
    private AVAudioEngine? _audioEngine;

    public override async Task<bool> RequestPermissionsAsync()
    {
        var speechStatus = await SFSpeechRecognizer.RequestAuthorizationAsync();
        if (speechStatus != SFSpeechRecognizerAuthorizationStatus.Authorized)
        {
            return false;
        }

        var audioSession = AVAudioSession.SharedInstance();
        var audioStatus = await audioSession.RequestRecordPermissionAsync();
        
        return audioStatus;
    }

    protected override async Task<bool> StartPlatformListeningAsync()
    {
        try
        {
            if (!await RequestPermissionsAsync())
            {
                OnErrorOccurred("Speech recognition permissions not granted");
                return false;
            }

            _speechRecognizer = new SFSpeechRecognizer(NSLocale.FromLocaleIdentifier("en-US"));
            if (_speechRecognizer == null || !_speechRecognizer.Available)
            {
                OnErrorOccurred("Speech recognizer not available");
                return false;
            }

            _audioEngine = new AVAudioEngine();
            _recognitionRequest = new SFSpeechAudioBufferRecognitionRequest();

            var inputNode = _audioEngine.InputNode;
            var recordingFormat = inputNode.GetBusOutputFormat(0);

            inputNode.InstallTapOnBus(0, 1024, recordingFormat, (buffer, when) =>
            {
                _recognitionRequest?.Append(buffer);
            });

            _audioEngine.Prepare();
            _audioEngine.StartAndReturnError(out var error);

            if (error != null)
            {
                OnErrorOccurred($"Audio engine error: {error.LocalizedDescription}");
                return false;
            }

            _recognitionTask = _speechRecognizer.GetRecognitionTask(_recognitionRequest, (result, err) =>
            {
                if (result != null)
                {
                    OnSpeechRecognized(result.BestTranscription.FormattedString);
                }

                if (err != null)
                {
                    OnErrorOccurred($"Recognition error: {err.LocalizedDescription}");
                    _isListening = false;
                }
            });

            return true;
        }
        catch (Exception ex)
        {
            OnErrorOccurred($"iOS speech recognition error: {ex.Message}");
            return false;
        }
    }

    protected override async Task StopPlatformListeningAsync()
    {
        try
        {
            _audioEngine?.Stop();
            _audioEngine?.InputNode.RemoveTapOnBus(0);
            _recognitionRequest?.EndAudio();
            _recognitionTask?.Cancel();

            _audioEngine = null;
            _recognitionRequest = null;
            _recognitionTask = null;
            _speechRecognizer = null;

            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            OnErrorOccurred($"Error stopping iOS speech recognition: {ex.Message}");
        }
    }
}
}
