import asyncio
from semantic_kernel import Kernel
from semantic_kernel.connectors.ai.open_ai import AzureChatCompletion
from semantic_kernel.prompt_template import PromptTemplateConfig
from semantic_kernel.prompt_template.handlebars_prompt_template import HandlebarsPromptTemplate

async def main():
    model_id = "gpt-4o-mini"
    endpoint = "https://openai202.openai.azure.com"
    api_key = ""  # Add your API key here

    # Create a kernel with Azure OpenAI chat completion
    kernel = Kernel()
    kernel.add_service(AzureChatCompletion(model_id, endpoint, api_key))

    handlebars_template = """
    <message role="system">You are an AI assistant designed to help with image recognition tasks.</message>
    <message role="user">
        <text>{{request}}</text>
        <image>{{imageData}}</image>
    </message>
    """

    template_config = PromptTemplateConfig(
        template=handlebars_template,
        template_format="handlebars",
        name="Vision_Chat_Prompt"
    )

    function = kernel.create_function_from_prompt(
        prompt_template_config=template_config,
        prompt_template_type=HandlebarsPromptTemplate
    )

    arguments = {
        "request": "Describe this image",
        "imageData": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAIAAAACUFjqAAAALElEQVR4nGKZzfeQAQk8tf6MzGViwAtoKs2yzFsImR/geYhudhOQBgQAAP//oeMGFCsVo7YAAAAASUVORK5CYII="
    }

    response = await kernel.invoke(function, arguments)
    print(response)

if __name__ == "__main__":
    asyncio.run(main())
