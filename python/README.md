# Semantic Kernel Python Demos

This directory contains Python implementations of the Semantic Kernel demos, mirroring the .NET versions in the `../dotnet/` directory.

## Prerequisites

- Python 3.10 or higher
- An Azure OpenAI account with API access
- Azure OpenAI API key and endpoint

## Setup

1. **Install dependencies:**

```bash
# Install all demo dependencies
pip install -r requirements.txt

# Or install individual demo dependencies
cd <demo-folder>
pip install -r requirements.txt
```

2. **Configure API credentials:**

Each demo requires your Azure OpenAI credentials. You can either:

- Edit the `main.py` file in each demo folder and add your credentials:
  ```python
  api_key = "your-api-key-here"
  endpoint = "https://your-resource.openai.azure.com"
  ```

- Set environment variables:
  ```bash
  export AZURE_OPENAI_API_KEY="your-api-key"
  export AZURE_OPENAI_ENDPOINT="https://your-resource.openai.azure.com"
  export AZURE_OPENAI_MODEL_ID="gpt-4o-mini"
  ```

- For the PlannerApp demo, copy `.env.example` to `.env` and add your credentials.

## Demos

### 1. Basic Prompt (`basic-prompt/`)
Simple prompt invocation to generate a list of breakfast foods.

**Run:**
```bash
cd basic-prompt
python main.py
```

### 2. Chat History (`chathistory/`)
Interactive chat application that maintains conversation history for a travel assistant.

**Run:**
```bash
cd chathistory
python main.py
```

### 3. Light Plugin (`LightPluginApp/`)
Demonstrates plugin functionality with a light control system using function calling.

**Run:**
```bash
cd LightPluginApp
python main.py
```

### 4. Handlebars Learning (`handlebars-1-shot-learning/`)
Few-shot learning example using Handlebars templates for flight information extraction.

**Run:**
```bash
cd handlebars-1-shot-learning
python main.py
```

### 5. Multiple Input Travel Guide (`multiple-input-travel-guide/`)
Shows how to use multiple input variables in prompts for personalized travel recommendations.

**Run:**
```bash
cd multiple-input-travel-guide
python main.py
```

### 6. Intent Investigation (`intent-investigation/`)
Classifies user intents from natural language requests.

**Run:**
```bash
cd intent-investigation
python main.py
```

### 7. Image Recognition Agent (`image-recognition-agent/`)
Demonstrates vision capabilities for describing images using base64 encoded data.

**Run:**
```bash
cd image-recognition-agent
python main.py
```

### 8. Planner App (`PlannerApp/`)
Advanced demo with streaming responses and function calling for math problem solving.

**Setup:**
```bash
cd PlannerApp
cp .env.example .env
# Edit .env with your credentials
```

**Run:**
```bash
python main.py
```

## Interactive Demo Launcher

To run an interactive demo launcher:

```bash
python main.py
```

This will display a menu of all available demos and allow you to run them interactively.

## Common Issues

### Import Errors
If you encounter import errors, make sure you've installed the required dependencies:
```bash
pip install semantic-kernel
```

### API Key Issues
- Ensure your Azure OpenAI credentials are correct
- Check that your API key has the necessary permissions
- Verify the endpoint URL matches your Azure OpenAI resource

### Module Not Found
Run from the project root or ensure you're in the correct demo directory:
```bash
cd python/<demo-name>
python main.py
```

## Additional Resources

- [Semantic Kernel Python Documentation](https://learn.microsoft.com/en-us/semantic-kernel/)
- [Azure OpenAI Service](https://azure.microsoft.com/en-us/products/ai-services/openai-service)
- [Semantic Kernel GitHub](https://github.com/microsoft/semantic-kernel)

## License

These demos are provided as educational examples for working with Semantic Kernel.
