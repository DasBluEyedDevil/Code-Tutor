using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
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

    private async Task<ExecutionResult> ExecuteCSharpAsync(string code)
    {
        // Check if dotnet-script is available
        var checkResult = await RunProcessAsync("dotnet", "tool list -g");
        if (!checkResult.Output.Contains("dotnet-script"))
        {
            return new ExecutionResult(false, "",
                "C# execution requires dotnet-script. Install with: dotnet tool install -g dotnet-script");
        }

        var tempFile = Path.GetTempFileName();
        var csharpFile = Path.ChangeExtension(tempFile, ".csx");
        File.Move(tempFile, csharpFile);

        await File.WriteAllTextAsync(csharpFile, code);

        try
        {
            var result = await RunProcessAsync("dotnet-script", csharpFile);
            return result;
        }
        finally
        {
            File.Delete(csharpFile);
        }
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

            // Read streams concurrently to avoid deadlock
            var outputTask = process.StandardOutput.ReadToEndAsync();
            var errorTask = process.StandardError.ReadToEndAsync();

            // Add timeout (30 seconds)
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            try
            {
                await process.WaitForExitAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                process.Kill(true);
                return new ExecutionResult(false, "", "Execution timed out after 30 seconds");
            }

            var output = await outputTask;
            var error = await errorTask;

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
