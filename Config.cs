using System;

namespace ReasoningAI;

public class Config
{
    public int MaxReasoningIterations { get; set; } = 8;
    public string ServerAddress { get; set; } = "http://localhost:11434/api/chat";
    public string ServerType { get; set; } = "OLLAMA";
    public string BaseModel { get; set; } = "ReasoningAI";
    public string SystemPromptFileName { get; set; } = Program.AppPath + "/model.txt";
    public int Animation { get; set; } = 1;
    public int MaxAnimationMsPerToken { get; set; } = 21;
}
