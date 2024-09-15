using System;

namespace ReasoningAI;

public class Config
{
    public int MaxReasoningIterations { get; set; } = 5;
    public string OllamaAddress { get; set; } = "http://localhost:11434/";
}
