using System;

namespace ReasoningAI;

public class Config
{
    public int MaxReasoningIterations { get; set; } = 5;
    public string OllamaAddress { get; set; } = "http://localhost:11434/";
    public int Animation { get; set; } = 1;
    public int MaxAnimationMsPerToken { get; set; } = 21;
}
