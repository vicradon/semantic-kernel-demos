import asyncio
from semantic_kernel import Kernel
from semantic_kernel.connectors.ai.open_ai import AzureChatCompletion

async def main():
    model_id = "gpt-4o-mini"
    endpoint = "https://openai202.openai.azure.com"
    api_key = ""  # Add your API key here

    kernel = Kernel()
    kernel.add_service(AzureChatCompletion(model_id, endpoint, api_key))

    result = await kernel.invoke_prompt("Give me a list of breakfast foods with eggs and cheese")
    print(result)

if __name__ == "__main__":
    asyncio.run(main())
