using System;
using System.Text.Json;

namespace ReasoningAI;

public class Chat
{
    public string ID { get; set; } = Guid.NewGuid().ToString();
    public List<Prompt> Prompts { get; set; } = new List<Prompt>();

    public Chat()
    { }

    public async Task<Prompt> Send(string prompt, string responseType, int maxIterations = 15)
    {
        Prompt p = new Prompt();
        p.UserPrompt = prompt;
        Prompts.Add(p);

        LLMRequest request = new LLMRequest();
        foreach (Prompt h in Prompts)
            request.messages.AddRange(h.GetMessages());
        request.messages.Add(new LLMMessage() { role = "user", content = prompt });

        ILLMResponse response = await request.Send(responseType);
        int i = 0;
        while (!p.AddResponse(response.GetMessage()) && ++i < maxIterations)
        {
            string continueToken = "<CONTINUE>";
            if (i == maxIterations - 1)
                continueToken = "<FINAL CONTINUE>";

            request.messages.Add(new LLMMessage() { role = "assistant", content = response.GetMessage() });
            request.messages.Add(new LLMMessage() { role = "user", content = "<ORIGINAL PROMPT>" + prompt + "</ORIGINAL PROMPT>" + continueToken });
            response = await request.Send(responseType);
        }
        request.messages.Add(new LLMMessage() { role = "assistant", content = response.GetMessage() });
        request.messages.Add(new LLMMessage() { role = "user", content = "<ORIGINAL PROMPT>" + prompt + "</ORIGINAL PROMPT><GENERATE RESPONSE>" });
        p.FinalResponse = (await request.Send(responseType)).GetMessage();

        return p;
    } 

    public void Save()
    {
        File.WriteAllText(Program.AppPath + "/chats/" + ID + ".json", JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true }));
    }
}
