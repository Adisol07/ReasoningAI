using System;
using System.Text.Json;

namespace ReasoningAI;

public class OllamaRequest
{
    public static string URL { get; set; } = "http://localhost:11434/api/chat";

    public string model { get; set; } = "ReasoningAI";
    public List<OllamaMessage> messages { get; set; } = new List<OllamaMessage>();
    public bool stream { get; set; } = false;

    public async Task<OllamaResponse> Send()
    {
        HttpClient client = new HttpClient();
        client.Timeout = TimeSpan.FromMinutes(10);

        StringContent json = new StringContent(JsonSerializer.Serialize(this));
        HttpResponseMessage response = await client.PostAsync(URL, json);

        response.EnsureSuccessStatusCode();

        string responseStr = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<OllamaResponse>(responseStr)!;
    }
}
