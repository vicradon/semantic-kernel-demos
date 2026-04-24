using DotNetEnv;
using Microsoft.SemanticKernel;

Env.Load();

var modelId = Environment.GetEnvironmentVariable("AZURE_OPENAI_MODEL_ID") ?? throw new InvalidOperationException("AZURE_OPENAI_MODEL_ID environment variable is not set");
var endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT") ?? throw new InvalidOperationException("AZURE_OPENAI_ENDPOINT environment variable is not set");
var apiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY") ?? throw new InvalidOperationException("AZURE_OPENAI_API_KEY environment variable is not set");

// Create a kernel with Azure OpenAI chat completion
var builder = Kernel.CreateBuilder().AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);

// Build the kernel
Kernel kernel = builder.Build();

string prompt = """
    You are a helpful travel guide. 
    I'm visiting {{$city}}. {{$background}}. What are some activities I should do today?
    """;
string city = "Barcelona";
string background = "I really enjoy art and dance.";

// Create the kernel function from the prompt
var activitiesFunction = kernel.CreateFunctionFromPrompt(prompt);

// Create the kernel arguments
var arguments = new KernelArguments { ["city"] = city, ["background"] = background };

// InvokeAsync on the kernel object
var result = await kernel.InvokeAsync(activitiesFunction, arguments);
Console.WriteLine(result);
