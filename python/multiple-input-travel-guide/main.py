import asyncio
from semantic_kernel import Kernel
from semantic_kernel.connectors.ai.open_ai import AzureChatCompletion

async def main():
    model_id = "gpt-4o-mini"
    endpoint = "https://openai202.openai.azure.com"
    api_key = ""  # Add your API key here

    # Create a kernel with Azure OpenAI chat completion
    kernel = Kernel()
    kernel.add_service(AzureChatCompletion(model_id, endpoint, api_key))

    prompt = """
    You are a helpful travel guide.
    I'm visiting {{$city}}. {{$background}}. What are some activities I should do today?
    """
    city = "Barcelona"
    background = "I really enjoy art and dance."

    # Create the kernel function from the prompt
    activities_function = kernel.create_function_from_prompt(prompt)

    # Create the kernel arguments
    arguments = {"city": city, "background": background}

    # InvokeAsync on the kernel object
    result = await kernel.invoke(activities_function, arguments)
    print(result)

if __name__ == "__main__":
    asyncio.run(main())
