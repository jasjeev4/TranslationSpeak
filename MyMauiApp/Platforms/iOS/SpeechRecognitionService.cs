using AVFoundation;
using Foundation;
using Speech;

public class SpeechRecognitionService
{
    private readonly AVAudioEngine audioEngine;
    private readonly SFSpeechRecognizer speechRecognizer;
    private SFSpeechAudioBufferRecognitionRequest? recognitionRequest;
    private SFSpeechRecognitionTask? recognitionTask;

    public SpeechRecognitionService()
    {
        audioEngine = new AVAudioEngine();
        speechRecognizer = new SFSpeechRecognizer(NSLocale.CurrentLocale)
                           ?? throw new Exception("Speech recognizer is not available.");
    }

    public void RequestAuthorization()
    {
        SFSpeechRecognizer.RequestAuthorization((authStatus) =>
        {
            if (authStatus != SFSpeechRecognizerAuthorizationStatus.Authorized)
            {
                throw new Exception("Speech recognition authorization denied.");
            }
        });
    }

    public void StartRecording(Action<string> onPartialResult)
    {
        var audioSession = AVAudioSession.SharedInstance();
        audioSession.SetCategory(AVAudioSessionCategory.Record);
        audioSession.SetMode(AVAudioSession.ModeMeasurement, out NSError modeError);
        audioSession.SetActive(true, out NSError activeError);

        if (modeError != null || activeError != null)
        {
            throw new Exception("Failed to configure audio session.");
        }

        recognitionRequest = new SFSpeechAudioBufferRecognitionRequest();
        recognitionTask = speechRecognizer.GetRecognitionTask(recognitionRequest, (result, error) =>
        {
            if (result != null)
            {
                onPartialResult(result.BestTranscription.FormattedString);
            }

            if (error != null)
            {
                Console.WriteLine($"Recognition error: {error.LocalizedDescription}");
            }
        });

        var inputNode = audioEngine.InputNode;
        if (inputNode == null)
        {
            throw new Exception("Audio engine has no input node.");
        }

        inputNode.InstallTapOnBus(0, 1024, inputNode.GetBusOutputFormat(0), (buffer, when) =>
        {
            recognitionRequest?.Append(buffer);
        });

        audioEngine.Prepare();
        audioEngine.StartAndReturnError(out NSError startError);

        if (startError != null)
        {
            throw new Exception($"Audio engine start failed: {startError.LocalizedDescription}");
        }
    }

    public void StopRecording()
    {
        audioEngine.Stop();
        audioEngine.InputNode.RemoveTapOnBus(0);
        recognitionRequest?.EndAudio();
        recognitionTask?.Cancel();
    }
}
