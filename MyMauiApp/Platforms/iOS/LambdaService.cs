using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class LambdaService
{
    private readonly HttpClient httpClient;

    public LambdaService()
    {
        httpClient = new HttpClient();
    }

    public async Task<string> GetAudioFromLambda(string transcription)
    {
        var lambdaUrl = Foundation.NSBundle.MainBundle.ObjectForInfoDictionary("TRANSLATION_API_KEY").ToString();
        Console.WriteLine($"Lambda URL: {lambdaUrl}");
        var requestUrl = $"{lambdaUrl}?text={Uri.EscapeDataString(transcription)}";

        var response = await httpClient.GetAsync(requestUrl);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync();
            throw new Exception($"Lambda call failed: {response.ReasonPhrase}. Error Body: {errorBody}");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Raw Lambda Response: {jsonResponse}");

        var json = JsonSerializer.Deserialize<JsonElement>(jsonResponse);
        return json.GetProperty("audio_base64").GetString();
    }

}
