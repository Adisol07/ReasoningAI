using System;

namespace ReasoningAI;

public class Prompt
{
    public static Action<string>? TitleChanged { get; set; }
    
    public string UserPrompt { get; set; } = "";
    public List<string> Reasonings { get; set; } = new List<string>();
    public string FinalResponse { get; set; } = "";

    // public static async Task<Prompt> Send(string prompt, int maxIterations = 15)
    // {
    //     Prompt p = new Prompt();
    //     p.UserPrompt = prompt;

    //     OllamaRequest request = new OllamaRequest();
    //     request.messages.Add(new OllamaMessage() { role = "user", content = prompt });

    //     OllamaResponse response = await request.Send();
    //     int i = 0;
    //     while (!p.AddResponse(response.message!.content) && ++i < maxIterations)
    //     {
    //         string continueToken = "<CONTINUE>";
    //         if (i == maxIterations - 1)
    //             continueToken = "<FINAL CONTINUE>";

    //         request.messages.Add(new OllamaMessage() { role = "assistant", content = response.message.content });
    //         request.messages.Add(new OllamaMessage() { role = "user", content = "<ORIGINAL PROMPT>" + prompt + "</ORIGINAL PROMPT>" + continueToken });
    //         response = await request.Send();
    //     }
    //     request.messages.Add(new OllamaMessage() { role = "assistant", content = response.message.content });
    //     request.messages.Add(new OllamaMessage() { role = "user", content = "<ORIGINAL PROMPT>" + prompt + "</ORIGINAL PROMPT><GENERATE RESPONSE>" });
    //     p.FinalResponse = (await request.Send()).message!.content;

    //     return p;
    // }

    public List<LLMMessage> GetMessages()
    {
        List<LLMMessage> messages = new List<LLMMessage>();

        messages.Add(new LLMMessage() { role = "user", content = UserPrompt });
        foreach (string msg in Reasonings)
        {
            messages.Add(new LLMMessage() { role = "assistant", content = msg });
        }

        return messages;
    }

    public bool AddResponse(string response)
    {
        if (response.Contains("<TITLE>") && response.Contains("</TITLE>"))
        {
            int index = response.IndexOf("<TITLE>");
            int endIndex = response.IndexOf("</TITLE>");
            string title = response.Substring(index + "<TITLE>".Length, endIndex - index - "<TITLE>".Length);
            if (TitleChanged != null)
                TitleChanged.Invoke(title);
        }

        Reasonings.Add(response);
        if (response.Contains("<FINISHED>"))
            return true;
        return false;
    }
}
