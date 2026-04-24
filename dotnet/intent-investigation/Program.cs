using DotNetEnv;
using Microsoft.SemanticKernel;

Env.Load();

var modelId = Environment.GetEnvironmentVariable("AZURE_OPENAI_MODEL_ID") ?? throw new InvalidOperationException("AZURE_OPENAI_MODEL_ID environment variable is not set");
var endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT") ?? throw new InvalidOperationException("AZURE_OPENAI_ENDPOINT environment variable is not set");
var apiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY") ?? throw new InvalidOperationException("AZURE_OPENAI_API_KEY environment variable is not set");

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

