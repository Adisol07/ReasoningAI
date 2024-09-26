using System;

namespace ReasoningAI;

public class LMStudioResponseChoice
{
    public int index { get; set; }
    public LLMMessage? message { get; set; }
    public string? logprobs { get; set; }
    public string? finish_reason { get; set; }
}
