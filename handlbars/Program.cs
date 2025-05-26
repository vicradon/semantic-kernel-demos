using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;

// Populate values from your OpenAI deployment
var modelId = "gpt-4o-mini";
var endpoint = "https://openai202.openai.azure.com";
var apiKey = "";

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
    {"request","Describe this image:"},
    {"imageData", "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAIAAAACUFjqAAAALElEQVR4nGKZzfeQAQk8tf6MzGViwAtoKs2yzFsImR/geYhudhOQBgQAAP//oeMGFCsVo7YAAAAASUVORK5CYII="}
});

var response = await kernel.InvokeAsync(function, arguments);
Console.WriteLine(response);
