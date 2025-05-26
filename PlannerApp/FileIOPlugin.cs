using System;
using System.ComponentModel;
using System.IO;
using Microsoft.SemanticKernel;

public class FileIOPlugin
{
    private readonly string _baseDirectory;

    public FileIOPlugin(string? baseDirectory = null)
    {
        // Use current directory if no base directory specified
        _baseDirectory = baseDirectory ?? Directory.GetCurrentDirectory();
    }

    [KernelFunction, Description("Writes content to a file, overwriting if it exists.")]
    public string WriteToFile(string fileName, string content)
    {
        try
        {
            var filePath = Path.Combine(_baseDirectory, fileName);
            File.WriteAllText(filePath, content);
            return $"Content written to '{fileName}'.";
        }
        catch (Exception ex)
        {
            return $"Failed to write to file: {ex.Message}";
        }
    }

    [KernelFunction, Description("Appends content to a file.")]
    public string AppendToFile(string fileName, string content)
    {
        try
        {
            var filePath = Path.Combine(_baseDirectory, fileName);
            File.AppendAllText(filePath, content);
            return $"Content appended to '{fileName}'.";
        }
        catch (Exception ex)
        {
            return $"Failed to append to file: {ex.Message}";
        }
    }

    [KernelFunction, Description("Reads the content of a file.")]
    public string ReadFile(string fileName)
    {
        try
        {
            var filePath = Path.Combine(_baseDirectory, fileName);
            if (!File.Exists(filePath)) return $"File '{fileName}' does not exist.";
            return File.ReadAllText(filePath);
        }
        catch (Exception ex)
        {
            return $"Failed to read file: {ex.Message}";
        }
    }

    [KernelFunction, Description("Deletes a file.")]
    public string DeleteFile(string fileName)
    {
        try
        {
            var filePath = Path.Combine(_baseDirectory, fileName);
            if (!File.Exists(filePath)) return $"File '{fileName}' does not exist.";
            File.Delete(filePath);
            return $"File '{fileName}' deleted.";
        }
        catch (Exception ex)
        {
            return $"Failed to delete file: {ex.Message}";
        }
    }
}
