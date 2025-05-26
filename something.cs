//#r "nuget: Microsoft.SemanticKernel, 1.30.0"
using Microsoft.SemanticKernel;

// Populate values from your OpenAI deployment
var modelId = "gpt-4o-mini";
var endpoint = "https://openai202.openai.azure.com";
var apiKey = "";

// Create a kernel with Azure OpenAI chat completion
var builder = Kernel.CreateBuilder().AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);

// Build the kernel
Kernel kernel = builder.Build();

// var result = await kernel.InvokePromptAsync("Give me a list of breakfast foods with eggs and cheese");
//Console.WriteLine(result);


string request = "I want to make a pdf";
string prompt = $"""
Instructions: What is the intent of this request?
If you don't know the intent, don't guess; instead respond with "Unknown".
Choices: SendEmail, SendMessage, CompleteTask, CreateDocument, Unknown.
User Input: {request}
Intent: 
""";

//var res2 = await kernel.InvokePromptAsync(prompt);
//Console.WriteLine(res2);


string city = "Rome";
var prompt2 = "I'm visiting {{$city}}. What are some activities I should do today?";

var activitiesFunction = kernel.CreateFunctionFromPrompt(prompt2);
var arguments = new KernelArguments { ["city"] = city };

// InvokeAsync on the KernelFunction object
var result = await activitiesFunction.InvokeAsync(kernel, arguments);
Console.WriteLine(result);

// InvokeAsync on the kernel object
result = await kernel.InvokeAsync(activitiesFunction, arguments);
Console.WriteLine(result);
