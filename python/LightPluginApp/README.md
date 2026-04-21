# Light Plugin App Demo

This demo shows how to create and use plugins with Semantic Kernel to extend AI capabilities with custom functions.

## What It Does

- Creates a `LightPlugin` class with functions to get and change light state
- Registers the plugin with the Semantic Kernel
- Uses an interactive chat interface where the AI can automatically call the light functions
- Demonstrates function calling - the AI decides when to use the plugin functions

## How to Run

1. Edit `main.py` and add your Azure OpenAI credentials:
   ```python
   endpoint = "https://your-resource.openai.azure.com"
   model_id = "gpt-4o-mini"
   api_key = "your-api-key"
   ```

2. Install dependencies:
   ```bash
   pip install -r requirements.txt
   ```

3. Run the demo:
   ```bash
   python main.py
   ```

4. Try these commands:
   - "Turn on the light"
   - "What's the current state of the light?"
   - "Turn off the light"
   - "Toggle the light"

## Key Concepts

- **Plugins**: Extensible functions that the AI can call
- **Function Calling**: AI automatically decides when to use available functions
- **Chat History**: Maintains conversation context
- **Decorators**: `@kernel_function` marks methods for AI usage

## Code Structure

```python
class LightPlugin:
    @kernel_function(description="Gets the state of the light.")
    def get_state(self) -> str:
        return "on" if self.is_on else "off"

    @kernel_function(description="Changes the state of the light.")
    def change_state(self, new_state: bool) -> str:
        self.is_on = new_state
        return self.get_state()

# Register plugin
kernel.add_plugin(LightPlugin(), plugin_name="LightPlugin")
```

## Features Demonstrated

- Custom plugin creation
- Function descriptions for AI understanding
- Automatic function calling
- Interactive chat with plugin integration
