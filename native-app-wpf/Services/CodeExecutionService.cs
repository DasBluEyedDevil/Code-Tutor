using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace CodeTutor.Wpf.Services;

public interface ICodeExecutionService
{
    Task<ExecutionResult> ExecuteAsync(string code, string language);
}

public record ExecutionResult(bool Success, string Output, string Error);

public class CodeExecutionService : ICodeExecutionService
{
    public async Task<ExecutionResult> ExecuteAsync(string code, string language)
    {
        return language.ToLower() switch
        {
            "python" => await ExecutePythonAsync(code),
            "javascript" => await ExecuteJavaScriptAsync(code),
            "csharp" => await ExecuteCSharpAsync(code),
            _ => new ExecutionResult(false, "", $"Language '{language}' not supported")
        };
    }

    private async Task<ExecutionResult> ExecutePythonAsync(string code)
    {
        var tempFile = Path.GetTempFileName() + ".py";
        await File.WriteAllTextAsync(tempFile, code);

        try
        {
            var result = await RunProcessAsync("python", tempFile);
            return result;
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    private async Task<ExecutionResult> ExecuteJavaScriptAsync(string code)
    {
        var tempFile = Path.GetTempFileName() + ".js";
        await File.WriteAllTextAsync(tempFile, code);

        try
        {
            var result = await RunProcessAsync("node", tempFile);
            return result;
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    private Task<ExecutionResult> ExecuteCSharpAsync(string code)
    {
        // C# execution requires dotnet-script or similar
        return Task.FromResult(new ExecutionResult(false, "", "C# execution coming soon"));
    }

    private async Task<ExecutionResult> RunProcessAsync(string command, string arguments)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = command,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            if (process == null)
                return new ExecutionResult(false, "", "Failed to start process");

            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            return new ExecutionResult(
                process.ExitCode == 0,
                output.Trim(),
                error.Trim()
            );
        }
        catch (Exception ex)
        {
            return new ExecutionResult(false, "", ex.Message);
        }
    }
}
