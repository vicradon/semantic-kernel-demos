using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

public class MathSolver
{
    [KernelFunction, Description("Solves a basic math expression like '5 + 3'.")]
    public string Solve(string expression)
    {
        try
        {
            var result = new System.Data.DataTable().Compute(expression, null);
            return $"{expression} = {result}";
        }
        catch
        {
            return $"Could not solve: {expression}";
        }
    }
}

class Program
{
    static async Task Main()
    {
        Console.WriteLine("======== Semantic Kernel Planner App ========");

        string? endpoint = "https://openai202.openai.azure.com";
        string? modelId = "gpt-4o-mini";
        string? apiKey = "";

        if (endpoint is null || modelId is null || apiKey is null)
        {
            Console.WriteLine("Azure OpenAI credentials not set.");
            return;
        }

        var builder = Kernel.CreateBuilder()
                            .AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);

        builder.Services.AddLogging(c => c.AddConsole().SetMinimumLevel(LogLevel.Information));
        builder.Plugins.AddFromType<MathSolver>();
        var kernel = builder.Build();

        var chatCompletion = kernel.GetRequiredService<IChatCompletionService>();
        var history = new ChatHistory();

        Console.Write("User > ");
        string? userInput;

        while ((userInput = Console.ReadLine()) is not null)
        {
            history.AddUserMessage(userInput);

            var settings = new OpenAIPromptExecutionSettings
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
            };

            var stream = chatCompletion.GetStreamingChatMessageContentsAsync(
                history, executionSettings: settings, kernel: kernel);

            string fullMessage = "";
            bool first = true;

            await foreach (var content in stream)
            {
                if (content.Role.HasValue && first)
                {
                    Console.Write("Assistant > ");
                    first = false;
                }

                Console.Write(content.Content);
                fullMessage += content.Content;
            }

            Console.WriteLine();
            history.AddAssistantMessage(fullMessage);
            Console.Write("User > ");
        }
    }
}
