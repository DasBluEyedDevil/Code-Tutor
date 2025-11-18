using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CodeTutor.Native.Models;

namespace CodeTutor.Native.Services;

/// <summary>
/// Executes code in various programming languages
/// NO HTTP, NO IPC - direct process spawning
/// </summary>
public class CodeExecutor
{
    private const int DefaultTimeoutMs = 10000; // 10 seconds

    /// <summary>
    /// Execute code in the specified language
    /// </summary>
    public async Task<ExecutionResult> ExecuteAsync(string language, string code)
    {
        return language.ToLower() switch
        {
            "python" => await ExecutePythonAsync(code),
            "javascript" => await ExecuteJavaScriptAsync(code),
            "java" => await ExecuteJavaAsync(code),
            "rust" => await ExecuteRustAsync(code),
            "csharp" or "c#" => await ExecuteCSharpAsync(code),
            _ => new ExecutionResult
            {
                Success = false,
                Error = $"Unsupported language: {language}"
            }
        };
    }

    private async Task<ExecutionResult> ExecutePythonAsync(string code)
    {
        var tempFile = Path.GetTempFileName() + ".py";
        try
        {
            await File.WriteAllTextAsync(tempFile, code);
            return await RunProcessAsync("python3", tempFile);
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }

    private async Task<ExecutionResult> ExecuteJavaScriptAsync(string code)
    {
        var tempFile = Path.GetTempFileName() + ".js";
        try
        {
            await File.WriteAllTextAsync(tempFile, code);
            return await RunProcessAsync("node", tempFile);
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }

    private async Task<ExecutionResult> ExecuteJavaAsync(string code)
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);

        try
        {
            // Extract class name from code
            var className = ExtractJavaClassName(code) ?? "Main";
            var javaFile = Path.Combine(tempDir, $"{className}.java");

            await File.WriteAllTextAsync(javaFile, code);

            // Compile
            var compileResult = await RunProcessAsync("javac", javaFile, tempDir);
            if (!compileResult.Success)
            {
                return compileResult;
            }

            // Run
            return await RunProcessAsync("java", className, tempDir);
        }
        finally
        {
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    private async Task<ExecutionResult> ExecuteRustAsync(string code)
    {
        var tempFile = Path.GetTempFileName() + ".rs";
        var exeFile = Path.ChangeExtension(tempFile, ".exe");

        try
        {
            await File.WriteAllTextAsync(tempFile, code);

            // Compile
            var compileResult = await RunProcessAsync("rustc", $"{tempFile} -o {exeFile}");
            if (!compileResult.Success)
            {
                return compileResult;
            }

            // Run
            return await RunProcessAsync(exeFile, "");
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
            if (File.Exists(exeFile)) File.Delete(exeFile);
        }
    }

    private async Task<ExecutionResult> ExecuteCSharpAsync(string code)
    {
        var tempFile = Path.GetTempFileName() + ".cs";
        try
        {
            await File.WriteAllTextAsync(tempFile, code);
            return await RunProcessAsync("dotnet", $"script {tempFile}");
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }

    /// <summary>
    /// Run a process and capture output
    /// </summary>
    private async Task<ExecutionResult> RunProcessAsync(string command, string arguments, string? workingDirectory = null)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WorkingDirectory = workingDirectory ?? Directory.GetCurrentDirectory()
                }
            };

            var outputBuilder = new StringBuilder();
            var errorBuilder = new StringBuilder();

            process.OutputDataReceived += (sender, e) =>
            {
                if (e.Data != null) outputBuilder.AppendLine(e.Data);
            };

            process.ErrorDataReceived += (sender, e) =>
            {
                if (e.Data != null) errorBuilder.AppendLine(e.Data);
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            var completed = await Task.Run(() => process.WaitForExit(DefaultTimeoutMs));

            if (!completed)
            {
                process.Kill();
                return new ExecutionResult
                {
                    Success = false,
                    Error = "Execution timed out",
                    ExecutionTimeMs = DefaultTimeoutMs
                };
            }

            stopwatch.Stop();

            var output = outputBuilder.ToString();
            var error = errorBuilder.ToString();

            return new ExecutionResult
            {
                Success = process.ExitCode == 0,
                Output = output,
                Error = error,
                ExitCode = process.ExitCode,
                ExecutionTimeMs = (int)stopwatch.ElapsedMilliseconds
            };
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            return new ExecutionResult
            {
                Success = false,
                Error = $"Execution failed: {ex.Message}",
                ExecutionTimeMs = (int)stopwatch.ElapsedMilliseconds
            };
        }
    }

    private string? ExtractJavaClassName(string code)
    {
        var match = System.Text.RegularExpressions.Regex.Match(code, @"public\s+class\s+(\w+)");
        return match.Success ? match.Groups[1].Value : null;
    }
}
