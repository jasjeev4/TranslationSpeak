using AVFoundation;
using Foundation;
using System;
using System.IO;

public class RecordingService
{
    private AVAudioRecorder recorder;
    private NSUrl audioFileUrl;

    public void StartRecording()
    {
        var audioSession = AVAudioSession.SharedInstance();
        audioSession.SetCategory(AVAudioSessionCategory.PlayAndRecord);
        audioSession.SetActive(true);

        var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var audioFilePath = Path.Combine(documents, "recording.m4a");
        audioFileUrl = NSUrl.FromFilename(audioFilePath);

        var settings = new AudioSettings
        {
            Format = AudioToolbox.AudioFormatType.MPEG4AAC,
            SampleRate = 44100,
            NumberChannels = 1,
            AudioQuality = AVAudioQuality.High
        };

        recorder = AVAudioRecorder.Create(audioFileUrl, settings, out NSError error);
        if (error != null)
            throw new Exception(error.Description);

        recorder.Record();
    }

    public string StopRecording()
    {
        recorder?.Stop();
        recorder = null;
        return audioFileUrl.Path;
    }
}
