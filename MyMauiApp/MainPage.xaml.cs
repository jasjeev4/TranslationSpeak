using System;
using Microsoft.Maui.Controls;

namespace MyMauiApp
{
    public partial class MainPage : ContentPage
    {
        private readonly SpeechRecognitionService speechRecognitionService;
        private readonly LambdaService lambdaService;
        private readonly AudioPlayerService audioPlayerService;
        private bool isRecording;

        public MainPage()
        {
            InitializeComponent();
            speechRecognitionService = new SpeechRecognitionService();
            lambdaService = new LambdaService();
            audioPlayerService = new AudioPlayerService();
            speechRecognitionService.RequestAuthorization();
            isRecording = false;
        }

        private async void OnRecordButtonClicked(object sender, EventArgs e)
        {
            if (isRecording)
            {
                // Stop Recording
                speechRecognitionService.StopRecording();
                RecordButton.Text = "Start Recording";
                isRecording = false;

                // Get the transcription and send it to the Lambda function
                var transcription = TranscriptionLabel.Text;
                if (!string.IsNullOrWhiteSpace(transcription))
                {
                    TranscriptionLabel.Text = "Processing translation...";
                    try
                    {
                        // Call Lambda function to get base64 MP3
                        var base64Audio = await lambdaService.GetAudioFromLambda(transcription);

                        // Save base64 audio to a file
                        var filePath = audioPlayerService.SaveBase64ToFile(base64Audio, "translated_audio.mp3");

                        // Play the audio file
                        audioPlayerService.Play(filePath);

                        TranscriptionLabel.Text = "Translation played successfully!";
                    }
                    catch (Exception ex)
                    {
                        TranscriptionLabel.Text = $"Error: {ex.Message}";
                    }
                }
                else
                {
                    TranscriptionLabel.Text = "No transcription available.";
                }
            }
            else
            {
                // Start Recording
                try
                {
                    TranscriptionLabel.Text = "Listening...";
                    speechRecognitionService.StartRecording((transcription) =>
                    {
                        // Update the UI with the transcription as it happens
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            TranscriptionLabel.Text = transcription;
                        });
                    });

                    RecordButton.Text = "Stop Recording";
                    isRecording = true;
                }
                catch (Exception ex)
                {
                    TranscriptionLabel.Text = $"Error: {ex.Message}";
                }
            }
        }
    }
}
