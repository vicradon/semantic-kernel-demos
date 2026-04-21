import os
import subprocess
import sys

def main():
    """Main entry point for Python Semantic Kernel demos."""
    print("Semantic Kernel Python Demos")
    print("=" * 50)
    print("\nAvailable demos:\n")

    demos = [
        ("1", "Basic Prompt", "basic-prompt", "Simple prompt invocation"),
        ("2", "Chat History", "chathistory", "Interactive chat with history tracking"),
        ("3", "Light Plugin", "LightPluginApp", "Plugin example with light control"),
        ("4", "Handlebars Learning", "handlebars-1-shot-learning", "Few-shot learning with Handlebars"),
        ("5", "Multiple Input Travel Guide", "multiple-input-travel-guide", "Prompt with multiple variables"),
        ("6", "Intent Investigation", "intent-investigation", "Intent classification example"),
        ("7", "Image Recognition Agent", "image-recognition-agent", "Vision capabilities with images"),
        ("8", "Planner App", "PlannerApp", "Math solver with streaming"),
    ]

    for num, name, folder, description in demos:
        print(f"{num}. {name:30s} - {description}")

    print("\n0. Exit")
    print("=" * 50)

    while True:
        choice = input("\nEnter demo number (0-8): ").strip()

        if choice == "0":
            print("Goodbye!")
            break

        demo_map = {
            "1": ("basic-prompt", "Simple prompt invocation for breakfast foods"),
            "2": ("chathistory", "Interactive travel assistant chat"),
            "3": ("LightPluginApp", "Light control with function calling"),
            "4": ("handlebars-1-shot-learning", "Flight extraction with few-shot learning"),
            "5": ("multiple-input-travel-guide", "Personalized travel recommendations"),
            "6": ("intent-investigation", "User request intent classification"),
            "7": ("image-recognition-agent", "Image description with vision"),
            "8": ("PlannerApp", "Math problem solver with plugins"),
        }

        if choice in demo_map:
            folder, description = demo_map[choice]
            print(f"\nRunning: {description}")
            print(f"Folder: {folder}")
            print("-" * 50)

            demo_path = os.path.join(os.path.dirname(__file__), folder, "main.py")
            if os.path.exists(demo_path):
                try:
                    subprocess.run([sys.executable, demo_path], check=True)
                except KeyboardInterrupt:
                    print("\nDemo interrupted.")
                except subprocess.CalledProcessError as e:
                    print(f"Error running demo: {e}")
            else:
                print(f"Demo file not found: {demo_path}")
        else:
            print("Invalid choice. Please enter a number between 0 and 8.")

        print("\n" + "=" * 50)
        print("Available demos:")
        for num, name, folder, description in demos:
            print(f"{num}. {name:30s} - {description}")
        print("0. Exit")

if __name__ == "__main__":
    main()
