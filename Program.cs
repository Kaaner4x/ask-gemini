using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

class Program
{
    static async Task Main(string[] args)
    {
        string? apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY");
        
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            Console.Write("Please enter your Gemini API Key: ");
            apiKey = ReadPassword();
            Console.WriteLine();
        }

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            Console.WriteLine("Error: API key was not provided. Exiting.");
            return;
        }

        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("x-goog-api-key", apiKey);

        string modelName = "gemini-3.6-flash";
        Console.WriteLine($"\nUSING MODEL: {modelName}\n");

        var history = new List<object>();

        Console.WriteLine("Chat started! Type 'exit' or 'quit' to close.\n");

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("You: ");
            Console.ResetColor();
            
            string? userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
                continue;

            if (userInput.ToLower() == "quit" || userInput.ToLower() == "exit")
            {
                break;
            }

            history.Add(new { role = "user", parts = new[] { new { text = userInput } } });

            var requestBody = new
            {
                contents = history
            };

            string jsonBody = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            try
            {
                string requestUrl = $"https://generativelanguage.googleapis.com/v1beta/models/{modelName}:generateContent?key={apiKey}";
                HttpResponseMessage response = await client.PostAsync(requestUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseJson = await response.Content.ReadAsStringAsync();
                    using JsonDocument doc = JsonDocument.Parse(responseJson);
                    
                    var candidates = doc.RootElement.GetProperty("candidates");
                    if (candidates.GetArrayLength() > 0)
                    {
                        var firstCandidate = candidates[0];
                        var contentObj = firstCandidate.GetProperty("content");
                        var parts = contentObj.GetProperty("parts");
                        var text = parts[0].GetProperty("text").GetString();

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Gemini: ");
                        Console.ResetColor();
                        Console.WriteLine(text);
                        Console.WriteLine();

                        history.Add(new { role = "model", parts = new[] { new { text = text } } });
                    }
                }
                else
                {
                    string errorDetail = await response.Content.ReadAsStringAsync();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("API Request failed. Status Code: " + response.StatusCode);
                    Console.WriteLine("Error Detail: " + errorDetail);
                    Console.ResetColor();
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An unexpected error occurred during the connection. Please check your internet connection.");
                Console.ResetColor();
            }
        }
    }

    static string ReadPassword()
    {
        string password = "";
        ConsoleKeyInfo info = Console.ReadKey(true);
        while (info.Key != ConsoleKey.Enter)
        {
            if (info.Key != ConsoleKey.Backspace)
            {
                password += info.KeyChar;
                Console.Write("*");
            }
            else if (info.Key == ConsoleKey.Backspace)
            {
                if (!string.IsNullOrEmpty(password))
                {
                    password = password.Substring(0, password.Length - 1);
                    int pos = Console.CursorLeft;
                    Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(pos - 1, Console.CursorTop);
                }
            }
            info = Console.ReadKey(true);
        }
        return password;
    }
}
