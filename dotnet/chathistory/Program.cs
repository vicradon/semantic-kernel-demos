using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

var modelId = "gpt-4o-mini";
var endpoint = "https://openai202.openai.azure.com";
var apiKey = "";

var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);

Kernel kernel = builder.Build();

var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
ChatHistory chatHistory = [];

void AddMessage(string msg) {
    Console.WriteLine(msg);
    chatHistory.AddAssistantMessage(msg);
}

void GetInput() {
    string input = Console.ReadLine()!;
    chatHistory.AddUserMessage(input);
}

async Task GetReply() {
    ChatMessageContent reply = await chatCompletionService.GetChatMessageContentAsync(
        chatHistory,
        kernel: kernel
    );
    Console.WriteLine(reply);
    chatHistory.AddAssistantMessage(reply.ToString());
}

chatHistory.AddSystemMessage("You are a helpful travel assistant.");
chatHistory.AddSystemMessage("Recommend a destination to the traveler based on their background and preferences.");

AddMessage("Tell me about your travel plans.");
GetInput();
await GetReply();

AddMessage("Would you like some activity recommendations?");
GetInput();
await GetReply();

AddMessage("Would you like some helpful phrases in the local language?");
GetInput();
await GetReply();

Console.WriteLine("Chat Ended.\n");
Console.WriteLine("Chat History:");

for (int i = 0; i < chatHistory.Count; i++)
{
    Console.WriteLine($"{chatHistory[i].Role}: {chatHistory[i]}");
}
