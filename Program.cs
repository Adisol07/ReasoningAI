using System;
using System.Text.Json;

namespace ReasoningAI;

class Program
{
    static string AppPath => AppDomain.CurrentDomain.BaseDirectory;

    static async Task Main()
    {
        Console.Title = "ReasoningAI";
        Config config = new Config();
        if (!File.Exists(AppPath + "/config.json"))
        {
            File.WriteAllText(AppPath + "/config.json", JsonSerializer.Serialize(config, new JsonSerializerOptions() { WriteIndented = true }));
        }
        config = JsonSerializer.Deserialize<Config>(File.ReadAllText(AppPath + "/config.json"))!;
        OllamaRequest.URL = config.OllamaAddress + "api/chat";
        Random rng = new Random();
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
            Prompt prompt = await Prompt.Send(userinput, config.MaxReasoningIterations);
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
