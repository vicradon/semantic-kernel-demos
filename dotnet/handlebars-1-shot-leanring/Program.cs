using DotNetEnv;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;
using Microsoft.SemanticKernel.ChatCompletion;

Env.Load();

var modelId = Environment.GetEnvironmentVariable("AZURE_OPENAI_MODEL_ID") ?? throw new InvalidOperationException("AZURE_OPENAI_MODEL_ID environment variable is not set");
var endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT") ?? throw new InvalidOperationException("AZURE_OPENAI_ENDPOINT environment variable is not set");
var apiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY") ?? throw new InvalidOperationException("AZURE_OPENAI_API_KEY environment variable is not set");

// Create a kernel with Azure OpenAI chat completion
var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);

// Build the kernel
Kernel kernel = builder.Build();

string prompt = """
    <message role="system">Instructions: Identify the customer's complaint details from MTN Nigeria customer's request.
    Extract: Data Plan Amount, Data Duration, Issue Type, and Customer Sentiment</message>

    <message role="user">I bought 2GB data plan yesterday and it just finished! I only watched YouTube for 30 minutes. MTN is stealing my money. This is unfair!</message>

    <message role="assistant">
    Data Plan Amount: 2GB
    Data Duration: 1 day
    Issue Type: Data depletion too fast
    Customer Sentiment: Angry, feels cheated
    </message>

    <message role="user">{{input}}</message>
    """;
   

string input = "I subscribed for 1.5GB weekly data last week. By Wednesday it was already showing 500MB remaining. I barely use Instagram and Twitter. This data is just disappearing. MTN needs to explain this.";

// Create the kernel arguments
var arguments = new KernelArguments { ["input"] = input };

// Create the prompt template config using handlebars format
var templateFactory = new HandlebarsPromptTemplateFactory();
var promptTemplateConfig = new PromptTemplateConfig()
{
    Template = prompt,
    TemplateFormat = "handlebars",
    Name = "MTNComplaintPrompt",
};

// Invoke the prompt function
var function = kernel.CreateFunctionFromPrompt(promptTemplateConfig, templateFactory);
var response = await kernel.InvokeAsync(function, arguments);
Console.WriteLine(response);



