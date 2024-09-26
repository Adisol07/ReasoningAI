using System;

namespace ReasoningAI;

public class LMStudioResponse : ILLMResponse
{
    public string? id { get; set; }
    public long created { get; set; }
    public string? model { get; set; }
    public List<LMStudioResponseChoice>? choices { get; set; }
    public LMStudioResponseUsage? usage { get; set; }
    public string? system_fingerprint { get; set; }

    public string GetMessage()
    {
        return choices![0].message!.content;
    }
}
