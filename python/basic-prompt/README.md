# Basic Prompt Demo

This demo demonstrates the simplest way to use Semantic Kernel - invoking a prompt to get a response from an AI model.

## What It Does

- Creates a basic Semantic Kernel with Azure OpenAI
- Invokes a simple prompt to generate a list of breakfast foods containing eggs and cheese
- Prints the AI's response

## How to Run

1. Edit `main.py` and add your Azure OpenAI credentials:
   ```python
   api_key = "your-api-key"
   endpoint = "https://your-resource.openai.azure.com"
   ```

2. Install dependencies:
   ```bash
   pip install -r requirements.txt
   ```

3. Run the demo:
   ```bash
   python main.py
   ```

## Key Concepts

- **Kernel**: The main orchestrator in Semantic Kernel
- **Prompt Invocation**: Simple way to get AI responses without complex setup
- **Azure OpenAI Integration**: Using Azure-hosted OpenAI models

## Code Structure

```python
kernel = Kernel()
kernel.add_service(AzureChatCompletion(model_id, endpoint, api_key))
result = await kernel.invoke_prompt("Your prompt here")
```
