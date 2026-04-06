using Microsoft.SemanticKernel;

var modelId = "gpt-4o-mini";
var endpoint = "https://openai202.openai.azure.com";
var apiKey = "";

var builder = Kernel.CreateBuilder().AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);
Kernel kernel = builder.Build();


string request = "I want to make a pdf";
string prompt = $"""
Instructions: What is the intent of this request?
If you don't know the intent, don't guess; instead respond with "Unknown".
Choices: SendEmail, SendMessage, CompleteTask, CreateDocument, Unknown.
User Input: {request}
Intent: 
""";

var intentFunction = kernel.CreateFunctionFromPrompt(prompt);
var result = await intentFunction.InvokeAsync(kernel);
Console.WriteLine(result);

