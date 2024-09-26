using System;
using System.Net.Http.Json;
using System.Text.Json;

namespace ReasoningAI;

public class LLMRequest
{
    public static string URL { get; set; } = "http://localhost:11434/api/chat";
    public static string SystemPrompt { get; set; } = "Something went wrong, write: `<FINISHED>`";

    public string model { get; set; } = "ReasoningAI";
    public List<LLMMessage> messages { get; set; } = new List<LLMMessage>();
    public bool stream { get; set; } = false;

    public async Task<ILLMResponse> Send(string responseType)
    {
        HttpClient client = new HttpClient();
        client.Timeout = TimeSpan.FromMinutes(10);

        // StringContent json = new StringContent(JsonSerializer.Serialize(this));
        if (messages[0].role != "system")
        {
            List<LLMMessage> msgs = [new LLMMessage() { role = "system", content = SystemPrompt }, ..messages];
            messages = msgs;
        }
        HttpResponseMessage response = await client.PostAsJsonAsync(URL, this);

        response.EnsureSuccessStatusCode();

        string responseStr = await response.Content.ReadAsStringAsync();
        if (responseType == "OLLAMA")
            return JsonSerializer.Deserialize<OllamaResponse>(responseStr)!;
        else if (responseType == "LM_STUDIO")
            return JsonSerializer.Deserialize<LMStudioResponse>(responseStr)!;
        else
            return null!;    
    }
}
