﻿using System;
using System.Text.Json;

namespace ReasoningAI;

class Program
{
    static async Task Main()
    {
        Console.Title = "ReasoningAI";
        Config config = new Config();
        if (!File.Exists("./config.json"))
        {
            File.WriteAllText("./config.json", JsonSerializer.Serialize(config, new JsonSerializerOptions() { WriteIndented = true }));
        }
        config = JsonSerializer.Deserialize<Config>(File.ReadAllText("./config.json"))!;
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
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Thinking..");
            Prompt.TitleChanged += (title)=>{
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
            foreach (char c in prompt.FinalResponse)
            {
                Console.Write(c);
                Thread.Sleep(rng.Next(0, 26));
            }
            Console.Write("\n");
        }
    }
}
