using System;

namespace ReasoningAI;

public class LMStudioResponseUsage
{
    public int prompt_tokens { get; set; }
    public int completion_tokens { get; set; }
    public int total_tokens { get; set; }
}
