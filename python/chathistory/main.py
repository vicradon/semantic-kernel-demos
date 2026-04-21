import asyncio
from semantic_kernel import Kernel
from semantic_kernel.connectors.ai.open_ai import AzureChatCompletion
from semantic_kernel.contents.chat_history import ChatHistory

async def main():
    model_id = "gpt-4o-mini"
    endpoint = "https://openai202.openai.azure.com"
    api_key = ""  # Add your API key here

    kernel = Kernel()
    kernel.add_service(AzureChatCompletion(model_id, endpoint, api_key))

    chat_completion_service = kernel.get_service("azure_chat_completion")
    chat_history = ChatHistory()

    def add_message(msg: str):
        print(msg)
        chat_history.add_assistant_message(msg)

    def get_input():
        user_input = input()
        chat_history.add_user_message(user_input)
        return user_input

    async def get_reply():
        reply = await chat_completion_service.get_chat_message_content(
            chat_history,
            kernel=kernel
        )
        print(reply)
        chat_history.add_assistant_message(str(reply))

    chat_history.add_system_message("You are a helpful travel assistant.")
    chat_history.add_system_message("Recommend a destination to the traveler based on their background and preferences.")

    add_message("Tell me about your travel plans.")
    get_input()
    await get_reply()

    add_message("Would you like some activity recommendations?")
    get_input()
    await get_reply()

    add_message("Would you like some helpful phrases in the local language?")
    get_input()
    await get_reply()

    print("Chat Ended.\n")
    print("Chat History:")

    for message in chat_history:
        print(f"{message.role}: {message.content}")

if __name__ == "__main__":
    asyncio.run(main())
