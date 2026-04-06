using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

public class LightPlugin
{
    public bool IsOn { get; set; } = false;

    [KernelFunction, Description("Gets the state of the light.")]
    public string GetState() => IsOn ? "on" : "off";

    [KernelFunction, Description("Changes the state of the light.")]
    public string ChangeState(bool newState)
    {
        IsOn = newState;
        Console.WriteLine($"[Light is now {(IsOn ? "on" : "off")}]");
        return GetState();
    }
}

class Program
{
    static async Task Main()
    {
        Console.WriteLine("======== Semantic Kernel Light Plugin App ========");

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

        builder.Plugins.AddFromType<LightPlugin>();
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

            var result = await chatCompletion.GetChatMessageContentAsync(
                history, executionSettings: settings, kernel: kernel);

            Console.WriteLine("Assistant > " + result?.Content);
            history.AddMessage(result?.Role ?? AuthorRole.Assistant, result?.Content ?? "");
            Console.Write("User > ");
        }
    }
}
