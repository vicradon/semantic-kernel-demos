import asyncio
from semantic_kernel import Kernel
from semantic_kernel.connectors.ai.open_ai import AzureChatCompletion, OpenAIChatCompletion
from semantic_kernel.contents import ChatHistory
from semantic_kernel.functions import kernel_function
from semantic_kernel.connectors.ai.function_calling_utils import kernel_function_exists


class LightPlugin:
    def __init__(self):
        self.is_on = False

    @kernel_function(
        description="Gets the state of the light."
    )
    def get_state(self) -> str:
        return "on" if self.is_on else "off"

    @kernel_function(
        description="Changes the state of the light."
    )
    def change_state(self, new_state: bool) -> str:
        self.is_on = new_state
        print(f"[Light is now {'on' if self.is_on else 'off'}]")
        return self.get_state()


async def main():
    print("======== Semantic Kernel Light Plugin App ========"")

    endpoint = "https://openai202.openai.azure.com"
    model_id = "gpt-4o-mini"
    api_key = ""  # Add your API key here

    if not endpoint or not model_id or not api_key:
        print("Azure OpenAI credentials not set.")
        return

    kernel = Kernel()
    kernel.add_service(AzureChatCompletion(model_id, endpoint, api_key))

    # Add the LightPlugin
    kernel.add_plugin(LightPlugin(), plugin_name="LightPlugin")

    chat_completion = kernel.get_service("azure_chat_completion")
    history = ChatHistory()

    print("User > ", end="")
    while True:
        user_input = input()
        if not user_input:
            break

        history.add_user_message(user_input)

        # Get response from the AI with function calling enabled
        response = await chat_completion.get_chat_message_content(
            history,
            kernel=kernel
        )

        print(f"Assistant > {response.content}")
        history.add_message(response.role, response.content or "")
        print("User > ", end="")

if __name__ == "__main__":
    asyncio.run(main())
