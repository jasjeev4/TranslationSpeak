# InstantTranslator

## Description

InstantTranslator is an iOS application built using .NET MAUI. It provides a seamless interface to record speech, transcribe it in real-time, and translate the text playback a speech audio file of the translated text using a Lambda service. The app is designed to be easy to use.

## Screenshots/Demo

## Installation

### Prerequisites

- [.NET MAUI](https://dotnet.microsoft.com/apps/maui)  (Minimum version: 9.0)
- Visual Studio with MAUI workload installed.
- Platforms supported: iOS.

### Steps

* Note: See environment variable configuration steps below 
1. Clone the repository:
   
   `git clone https://github.com/jasjeev4/InstantTranslator.git
   cd MyMauiApp` 

2. Restore dependencies:
   
   `dotnet restore` 

3. Build the project:
   
   `dotnet build` 

4. Run the application on your desired platform:
   
   `dotnet run --framework <TargetFramework>` 

## Environment Configuration

To use the app, you need to configure the following environment variable:

- **`TRANSLATION_API_KEY`** : This key is required for accessing the translation service.

You need to configure the `Info.plist` file as follows:

- **Locate the `Info.plist` file**: Navigate to the `Platforms/iOS/Info.plist` file in your project.

- **Add the API Key**: Add the following key-value pair to the `Info.plist` file:
  
  xml
  
  Copy code
  
  `<key>TRANSLATION_API_KEY</key> <string>your_api_key</string>`

- **Save the File**: After adding the key-value pair, save the `Info.plist` file.

# 

## Usage

1. Launch the app on your device or emulator.
2. Tap the  **Start Recording**  button to begin capturing audio.
3. Speak into the microphone; the transcription will appear on the screen in real-time.
4. Stop recording by tapping the button again.
5. The app processes the transcription and generates a translated audio file.
6. The translated audio is played automatically.

## Features

- **Speech Recognition**: Real-time transcription of audio input.
- **Lambda Integration**: Transcribes text and translates it into an audio file.
- **Audio Playback**: Plays the generated audio file.
- **Cross-Platform**: Supports iOS.

## Technologies Used

- **.NET MAUI**: Framework for cross-platform app development.
- **SpeechRecognitionService**: For audio transcription.
- **LambdaService**: Integration with AWS Lambda for text-to-speech translation.
- **AudioPlayerService**: For audio playback.

## Contributing

Contributions are welcome! Please:

1. Fork the repository.
2. Create a feature branch:  `git checkout -b feature/your-feature`.
3. Commit changes:  `git commit -m 'Add your feature'`.
4. Push to the branch:  `git push origin feature/your-feature`.
5. Open a pull request.

## Known Issues

- Transcription accuracy is affected by microphone quality and ambient noise.
- Limited support for some devices or SDK versions.
- Error handling needs improvement.

## Roadmap

- Support for additional languages.
- Enhanced UI/UX.

## Testing

1. Run the test suite:
   
   `dotnet test` 

2. View test coverage reports:
   
   `dotnet test --collect:"XPlat Code Coverage"` 

## References/Documentation

- [.NET MAUI Documentation](https://learn.microsoft.com/en-us/dotnet/maui/)
- [AWS Lambda Documentation](https://docs.aws.amazon.com/lambda/index.html)