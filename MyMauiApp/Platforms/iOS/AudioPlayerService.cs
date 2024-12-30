using AVFoundation;
using Foundation;
using System.IO;

public class AudioPlayerService
{
    private AVPlayer player;

    public void Play(string filePath)
    {
        var url = NSUrl.FromFilename(filePath);
        player = AVPlayer.FromUrl(url);
        player.Play();
    }

    public string SaveBase64ToFile(string base64Audio, string fileName)
    {
        var audioBytes = Convert.FromBase64String(base64Audio);
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
        File.WriteAllBytes(filePath, audioBytes);
        return filePath;
    }
}
