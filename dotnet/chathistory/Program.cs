using DotNetEnv;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

Env.Load();

var modelId = Environment.GetEnvironmentVariable("AZURE_OPENAI_MODEL_ID") ?? throw new InvalidOperationException("AZURE_OPENAI_MODEL_ID environment variable is not set");
var endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT") ?? throw new InvalidOperationException("AZURE_OPENAI_ENDPOINT environment variable is not set");
var apiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY") ?? throw new InvalidOperationException("AZURE_OPENAI_API_KEY environment variable is not set");

var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);

Kernel kernel = builder.Build();

var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
ChatHistory chatHistory = [];

void AddMessage(string msg) {
    Console.WriteLine($"Assistant: {msg}");
    chatHistory.AddAssistantMessage(msg);
}

void GetInput() {
    Console.Write("Customer > ");
    string input = Console.ReadLine()!;
    chatHistory.AddUserMessage(input);
}

async Task GetReply() {
    ChatMessageContent reply = await chatCompletionService.GetChatMessageContentAsync(
        chatHistory,
        kernel: kernel
    );
    Console.WriteLine($"Assistant: {reply}");
    chatHistory.AddAssistantMessage(reply.ToString());
}

chatHistory.AddSystemMessage("You are a helpful MTN Nigeria customer service assistant.");
chatHistory.AddSystemMessage("Help customers resolve their mobile data complaints and provide technical support.");

AddMessage("Welcome to MTN Customer Care! How can I help you with your data concerns today?");
GetInput();
await GetReply();

AddMessage("I understand your frustration. Can you tell me more about your data plan and usage?");
GetInput();
await GetReply();

AddMessage("Let me help you troubleshoot this issue. What apps do you use most frequently?");
GetInput();
await GetReply();

Console.WriteLine("Chat Ended.\n");
Console.WriteLine("Chat History:");

for (int i = 0; i < chatHistory.Count; i++)
{
    string role = chatHistory[i].Role == AuthorRole.User ? "Customer" : "Assistant";
    Console.WriteLine($"{role}: {chatHistory[i].Content}");
}
