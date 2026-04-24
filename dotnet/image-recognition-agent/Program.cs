using DotNetEnv;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;

Env.Load();

var modelId = Environment.GetEnvironmentVariable("AZURE_OPENAI_MODEL_ID") ?? throw new InvalidOperationException("AZURE_OPENAI_MODEL_ID environment variable is not set");
var endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT") ?? throw new InvalidOperationException("AZURE_OPENAI_ENDPOINT environment variable is not set");
var apiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY") ?? throw new InvalidOperationException("AZURE_OPENAI_API_KEY environment variable is not set");

// Create a kernel with Azure OpenAI chat completion
var builder = Kernel.CreateBuilder().AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);

// Build the kernel
Kernel kernel = builder.Build();

const string HandlebarsTemplate = """
    <message role="system">You are an AI assistant designed to help with image recognition tasks.</message>
    <message role="user">
        <text>{{request}}</text>
        <image>{{imageData}}</image>
    </message>
    """;

var templateFactory = new HandlebarsPromptTemplateFactory();
var promptTemplateConfig = new PromptTemplateConfig()
{
    Template = HandlebarsTemplate,
    TemplateFormat = "handlebars",
    Name = "Vision_Chat_Prompt",
};

var function = kernel.CreateFunctionFromPrompt(promptTemplateConfig, templateFactory);

var arguments = new KernelArguments(new Dictionary<string, object?>
{
    {"request","Describe this image"},
    {"imageData", "https://www.mypetsies.com/blog/app/uploads/2016/08/85120553.jpg"}
});

var response = await kernel.InvokeAsync(function, arguments);
Console.WriteLine(response);



