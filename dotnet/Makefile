# Makefile for Semantic Kernel Demos

# Define all project directories
PROJECTS := basic-prompt chathistory handlebars-1-shot-leanring image-recognition-agent intent-investigation functions-and-tools multiple-input-travel-guide LightPluginApp PlannerApp

# Default target
.PHONY: all
all: build

# Build all projects
.PHONY: build
build:
	@echo "Building all projects..."
	@for project in $(PROJECTS); do \
		echo "Building $$project..."; \
		cd $$project && dotnet build && cd ..; \
	done
	@echo "All projects built successfully!"

# Run all projects
.PHONY: run
run:
	@echo "Running all projects..."
	@for project in $(PROJECTS); do \
		echo ""; \
		echo "=========================================="; \
		echo "Running $$project"; \
		echo "=========================================="; \
		cd $$project && dotnet run && cd ..; \
	done
	@echo "All projects executed!"

# Run individual project
.PHONY: run-%
run-%:
	@echo "Running $*..."
	@cd $* && dotnet run

# Clean all projects
.PHONY: clean
clean:
	@echo "Cleaning all projects..."
	@for project in $(PROJECTS); do \
		echo "Cleaning $$project..."; \
		cd $$project && dotnet clean && cd ..; \
	done
	@echo "All projects cleaned!"

# Restore all dependencies
.PHONY: restore
restore:
	@echo "Restoring dependencies for all projects..."
	@for project in $(PROJECTS); do \
		echo "Restoring $$project..."; \
		cd $$project && dotnet restore && cd ..; \
	done
	@echo "All dependencies restored!"

# List all available projects
.PHONY: list
list:
	@echo "Available projects:"
	@for project in $(PROJECTS); do \
		echo "  - $$project"; \
	done

# Help target
.PHONY: help
help:
	@echo "Available targets:"
	@echo "  make build       - Build all projects"
	@echo "  make run         - Run all projects"
	@echo "  make run-<project> - Run specific project (e.g., make run-basic-prompt)"
	@echo "  make clean       - Clean all projects"
	@echo "  make restore     - Restore dependencies for all projects"
	@echo "  make list        - List all available projects"
	@echo "  make help        - Show this help message"
