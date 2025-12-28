using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
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
            "javascript" or "js" => await ExecuteJavaScriptAsync(code),
            "csharp" or "c#" => await ExecuteCSharpAsync(code),
            "java" => await ExecuteJavaAsync(code),
            "kotlin" => await ExecuteKotlinAsync(code),
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

    private async Task<ExecutionResult> ExecuteJavaAsync(string code)
    {
        // Extract class name from code (assumes public class Main or similar)
        var className = "Main";
        var classMatch = Regex.Match(code, @"public\s+class\s+(\w+)");
        if (classMatch.Success)
        {
            className = classMatch.Groups[1].Value;
        }

        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

        try
        {
            Directory.CreateDirectory(tempDir);
            var javaFile = Path.Combine(tempDir, $"{className}.java");
            await File.WriteAllTextAsync(javaFile, code);

            // Compile
            var compileResult = await RunProcessAsync("javac", javaFile);
            if (!compileResult.Success)
            {
                return new ExecutionResult(false, "", compileResult.Error);
            }

            // Run
            var result = await RunProcessAsync("java", $"-cp \"{tempDir}\" {className}");
            return result;
        }
        finally
        {
            if (Directory.Exists(tempDir))
            {
                try { Directory.Delete(tempDir, true); } catch { /* ignore cleanup errors */ }
            }
        }
    }

    private async Task<ExecutionResult> ExecuteKotlinAsync(string code)
    {
        var tempFile = Path.GetTempFileName();
        var kotlinFile = Path.ChangeExtension(tempFile, ".kts");
        File.Move(tempFile, kotlinFile);

        await File.WriteAllTextAsync(kotlinFile, code);

        try
        {
            // Kotlin script mode (.kts files run directly)
            var result = await RunProcessAsync("kotlinc", $"-script \"{kotlinFile}\"");
            return result;
        }
        finally
        {
            File.Delete(kotlinFile);
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
