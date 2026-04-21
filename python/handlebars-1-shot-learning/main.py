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

    prompt = """
    <message role="system">Instructions: Identify the from and to destinations
    and dates from the user's request</message>

    <message role="user">Can you give me a list of flights from Seattle to Tokyo?
    I want to travel from March 11 to March 18.</message>

    <message role="assistant">
    Origin: Seattle
    Destination: Tokyo
    Depart: 03/11/2025
    Return: 03/18/2025
    </message>

    <message role="user">{{input}}</message>
    """

    input_text = "I want to travel from June 1 to July 22. I want to go to Greece. I live in Chicago."

    # Create the prompt template config using handlebars format
    template_config = PromptTemplateConfig(
        template=prompt,
        template_format="handlebars",
        name="FlightPrompt"
    )

    # Create the prompt function
    function = kernel.create_function_from_prompt(
        prompt_template_config=template_config,
        prompt_template_type=HandlebarsPromptTemplate
    )

    # Create the kernel arguments
    arguments = {"input": input_text}

    # Invoke the prompt function
    response = await kernel.invoke(function, arguments)
    print(response)

if __name__ == "__main__":
    asyncio.run(main())
