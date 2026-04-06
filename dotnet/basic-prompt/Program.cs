using Microsoft.SemanticKernel;

var modelId = "gpt-4o-mini";
var endpoint = "https://openai202.openai.azure.com";
var apiKey = "";

var builder = Kernel.CreateBuilder().AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);

Kernel kernel = builder.Build();

var result = await kernel.InvokePromptAsync("Give me a list of breakfast foods with eggs and cheese");
Console.WriteLine(result);


