# Voice to Text Converter

A cross-platform voice-to-text application built with .NET MAUI that works on Windows, Android, and iOS.

## Features

- **Real-time Speech Recognition**: Convert spoken English to text in real-time
- **Cross-Platform**: Works on Windows, Android, and iOS
- **Simple UI**: Clean and intuitive interface with start/stop recording functionality
- **Text Management**: Copy recognized text to clipboard and clear text functionality
- **Permission Handling**: Automatic microphone permission requests

## Requirements

- .NET 8.0 or later
- Visual Studio 2022 17.8+ or Visual Studio Code with C# Dev Kit
- For Android: Android SDK API 21+
- For iOS: iOS 11.0+
- For Windows: Windows 10 version 1809+

## Getting Started

### 1. Clone or Download the Project

```bash
git clone <your-repo-url>
cd VoiceToText
```

### 2. Restore NuGet Packages

```bash
dotnet restore
```

### 3. Build the Project

```bash
dotnet build
```

### 4. Run the Application

#### For Windows:
```bash
dotnet run --framework net8.0-windows10.0.19041.0
```

#### For Android (with device/emulator connected):
```bash
dotnet run --framework net8.0-android
```

#### For iOS (requires Mac with Xcode):
```bash
dotnet run --framework net8.0-ios
```

## How to Use

1. **Launch the App**: Open the Voice to Text Converter application
2. **Grant Permissions**: Allow microphone access when prompted
3. **Start Recording**: Tap the "🎤 Start Recording" button
4. **Speak**: Speak clearly in English - your words will appear in the text area
5. **Stop Recording**: Tap the "🛑 Stop Recording" button when finished
6. **Copy Text**: Use the "📋 Copy Text" button to copy the recognized text to clipboard
7. **Clear Text**: Use the "🗑️ Clear Text" button to clear the text area

## Platform-Specific Features

### Windows
- Uses Windows Speech Recognition API
- Continuous speech recognition
- High accuracy for English speech

### Android
- Uses Android SpeechRecognizer
- Requires RECORD_AUDIO permission
- Supports partial results during recognition

### iOS
- Uses iOS Speech Framework
- Requires microphone and speech recognition permissions
- Real-time transcription with high accuracy

## Permissions

The app requires the following permissions:

### Android
- `RECORD_AUDIO`: To access the microphone for speech recognition
- `INTERNET`: For potential cloud-based speech recognition features

### iOS
- `NSMicrophoneUsageDescription`: To access the microphone
- `NSSpeechRecognitionUsageDescription`: To use speech recognition services

### Windows
- Microphone access is handled through Windows privacy settings

## Troubleshooting

### Common Issues

1. **No Speech Recognition**: 
   - Ensure microphone permissions are granted
   - Check that your device has a working microphone
   - Verify internet connection (some platforms may use cloud services)

2. **App Won't Start**:
   - Ensure you have the correct .NET version installed
   - Check that all NuGet packages are restored
   - Verify target platform SDK is installed

3. **Poor Recognition Accuracy**:
   - Speak clearly and at a moderate pace
   - Ensure you're in a quiet environment
   - Check microphone quality and positioning

### Platform-Specific Issues

#### Android
- If permissions are denied, go to Settings > Apps > Voice to Text > Permissions and enable Microphone
- Some Android devices may require additional setup for speech recognition

#### iOS
- Speech recognition requires an internet connection on iOS
- Ensure Speech Recognition is enabled in Settings > Privacy & Security > Speech Recognition

#### Windows
- Check Windows Speech Recognition settings in Control Panel
- Ensure microphone is set as default recording device

## Development

### Project Structure

```
VoiceToText/
├── Services/
│   ├── ISpeechRecognitionService.cs    # Interface for speech recognition
│   └── SpeechRecognitionService.cs     # Base implementation
├── Platforms/
│   ├── Android/
│   │   ├── SpeechRecognitionService.android.cs  # Android implementation
│   │   └── AndroidManifest.xml                  # Android permissions
│   ├── iOS/
│   │   ├── SpeechRecognitionService.ios.cs      # iOS implementation
│   │   └── Info.plist                           # iOS permissions
│   └── Windows/
│       └── SpeechRecognitionService.windows.cs  # Windows implementation
├── Resources/                          # App resources (icons, fonts, styles)
├── MainPage.xaml                      # Main UI layout
├── MainPage.xaml.cs                   # Main UI code-behind
└── MauiProgram.cs                     # App configuration
```

### Adding New Features

1. **Extend ISpeechRecognitionService**: Add new methods to the interface
2. **Update Platform Implementations**: Implement new methods in each platform-specific file
3. **Update UI**: Modify MainPage.xaml and MainPage.xaml.cs as needed

## License

This project is open source and available under the MIT License.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Support

If you encounter any issues or have questions, please create an issue in the project repository.
