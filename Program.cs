using System;
using System.Text.Json;

namespace ReasoningAI;

class Program
{
    public static string AppPath => AppDomain.CurrentDomain.BaseDirectory;

    static async Task Main()
    {
        Console.Title = "ReasoningAI";
        Config config = new Config();
        if (!File.Exists(AppPath + "/config.json"))
            File.WriteAllText(AppPath + "/config.json", JsonSerializer.Serialize(config, new JsonSerializerOptions() { WriteIndented = true }));
        if (!Directory.Exists(AppPath + "/chats"))
            Directory.CreateDirectory(AppPath + "/chats");
        config = JsonSerializer.Deserialize<Config>(File.ReadAllText(AppPath + "/config.json"))!;
        if (File.Exists(config.SystemPromptFileName))
            LLMRequest.SystemPrompt = File.ReadAllText(config.SystemPromptFileName);
        LLMRequest.URL = config.ServerAddress;
        Random rng = new Random();
        Chat chat = new Chat();
        while (true)
        {
            Console.Title = "ReasoningAI : New Chat";
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" > ");
            string userinput = Console.ReadLine()!;
            Console.Title = "ReasoningAI : " + userinput;
            DateTime start = DateTime.Now;
            int y = Console.GetCursorPosition().Top;
            if (y == Console.WindowHeight - 1)
                y--;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Thinking..");
            Prompt.TitleChanged += (title) =>
            {
                Console.SetCursorPosition(0, y);
                for (int x = 0; x < Console.WindowWidth - 1; x++)
                    Console.Write(" ");
                Console.SetCursorPosition(0, y);
                Console.WriteLine("Thinking: " + title);
            };
            Prompt prompt = await chat.Send(userinput, config.ServerType, config.MaxReasoningIterations);
            chat.Save();
            Console.SetCursorPosition(0, y);
            for (int x = 0; x < Console.WindowWidth - 1; x++)
                Console.Write(" ");
            Console.SetCursorPosition(0, y);
            Console.WriteLine("Thought for " + Math.Floor((DateTime.Now - start).TotalSeconds * 100) / 100 + " seconds");
            Console.ForegroundColor = ConsoleColor.Gray;
            if (config.Animation == 1)
            {
                foreach (char c in prompt.FinalResponse)
                {
                    Console.Write(c);
                    Thread.Sleep(rng.Next(0, config.MaxAnimationMsPerToken));
                }
            }
            else if (config.Animation == 2)
            {
                foreach (string word in prompt.FinalResponse.Split(' '))
                {
                    Console.Write(word + " ");
                    Thread.Sleep(rng.Next(0, config.MaxAnimationMsPerToken));
                }
            }
            else
            {
                Console.Write(prompt.FinalResponse);
            }
            Console.Write("\n");
        }
    }
}
