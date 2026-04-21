import asyncio
import os
from dotenv import load_dotenv
from semantic_kernel import Kernel
from semantic_kernel.connectors.ai.open_ai import AzureChatCompletion
from semantic_kernel.contents import ChatHistory
from semantic_kernel.functions import kernel_function

# Load environment variables
load_dotenv()

class MathSolver:
    @kernel_function(
        description="Solves a basic math expression like '5 + 3'."
    )
    def solve(self, expression: str) -> str:
        try:
            # Using eval for simple math expressions (caution: only use with trusted input)
            result = eval(expression)
            return f"{expression} = {result}"
        except Exception as e:
            return f"Could not solve: {expression}"

async def main():
    print("======== Semantic Kernel Planner App ========")

    endpoint = os.getenv("AZURE_OPENAI_ENDPOINT")
    model_id = os.getenv("AZURE_OPENAI_MODEL_ID")
    api_key = os.getenv("AZURE_OPENAI_API_KEY")

    if not endpoint or not model_id or not api_key:
        print("Azure OpenAI credentials not set.")
        print("Please set AZURE_OPENAI_ENDPOINT, AZURE_OPENAI_MODEL_ID, and AZURE_OPENAI_API_KEY environment variables.")
        return

    kernel = Kernel()
    kernel.add_service(AzureChatCompletion(model_id, endpoint, api_key))

    # Add the MathSolver plugin
    kernel.add_plugin(MathSolver(), plugin_name="MathSolver")

    chat_completion = kernel.get_service("azure_chat_completion")
    history = ChatHistory()

    print("User > ", end="")
    while True:
        user_input = input()
        if not user_input:
            break

        history.add_user_message(user_input)

        # Get streaming response from the AI with function calling enabled
        stream = chat_completion.get_streaming_chat_message_contents(
            history,
            kernel=kernel
        )

        full_message = ""
        first = True

        async for content in stream:
            if first:
                print("Assistant > ", end="")
                first = False
            print(content.content, end="", flush=True)
            full_message += content.content or ""

        print()
        history.add_assistant_message(full_message)
        print("User > ", end="")

if __name__ == "__main__":
    asyncio.run(main())
