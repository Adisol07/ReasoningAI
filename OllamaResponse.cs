using System;

namespace ReasoningAI;

public class OllamaResponse : ILLMResponse
{
    public string? model { get; set; }
    public DateTime? created_at { get; set; }
    public LLMMessage? message { get; set; }
    public bool? done { get; set; }
    public long? total_duration { get; set; }
    public long? load_duration { get; set; }
    public long? prompt_eval_count { get; set; }
    public long? prompt_eval_duration { get; set; }
    public long? eval_count { get; set; }
    public long? eval_duration { get; set; }

    public string GetMessage()
    {
        return this.message!.content;
    }
}

// {
//   "model": "llama3.1",
//   "created_at": "2023-12-12T14:13:43.416799Z",
//   "message": {
//     "role": "assistant",
//     "content": "Hello! How are you today?"
//   },
//   "done": true,
//   "total_duration": 5191566416,
//   "load_duration": 2154458,
//   "prompt_eval_count": 26,
//   "prompt_eval_duration": 383809000,
//   "eval_count": 298,
//   "eval_duration": 4799921000
// }
