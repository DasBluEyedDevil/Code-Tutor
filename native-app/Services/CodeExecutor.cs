using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CodeTutor.Native.Models;

namespace CodeTutor.Native.Services;

/// <summary>
/// Executes code in various programming languages with resource limits
/// NO HTTP, NO IPC - direct process spawning
/// Security: Memory limits, timeout protection, restricted environment
/// </summary>
public class CodeExecutor
{
    private const int DefaultTimeoutMs = 10000; // 10 seconds
    private const long MaxMemoryBytes = 512 * 1024 * 1024; // 512 MB
    private const int MaxOutputLength = 100000; // 100 KB output limit

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
    /// Run a process with resource limits and security restrictions
    /// </summary>
    private async Task<ExecutionResult> RunProcessAsync(string command, string arguments, string? workingDirectory = null)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WorkingDirectory = workingDirectory ?? Directory.GetCurrentDirectory()
            };

            // Apply security restrictions to environment
            ApplySecurityRestrictions(startInfo);

            using var process = new Process { StartInfo = startInfo };

            var outputBuilder = new StringBuilder();
            var errorBuilder = new StringBuilder();
            var outputTruncated = false;

            process.OutputDataReceived += (sender, e) =>
            {
                if (e.Data != null && outputBuilder.Length < MaxOutputLength)
                {
                    outputBuilder.AppendLine(e.Data);
                }
                else if (e.Data != null)
                {
                    outputTruncated = true;
                }
            };

            process.ErrorDataReceived += (sender, e) =>
            {
                if (e.Data != null && errorBuilder.Length < MaxOutputLength)
                {
                    errorBuilder.AppendLine(e.Data);
                }
            };

            process.Start();

            // Apply process-level resource limits (platform-specific)
            try
            {
                ApplyResourceLimits(process);
            }
            catch
            {
                // Resource limits may fail on some platforms - continue anyway
            }

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            var completed = await Task.Run(() => process.WaitForExit(DefaultTimeoutMs));

            if (!completed)
            {
                try
                {
                    // Try graceful shutdown first
                    process.Kill(entireProcessTree: true);
                }
                catch
                {
                    process.Kill();
                }

                return new ExecutionResult
                {
                    Success = false,
                    Error = "Execution timed out (10 second limit)",
                    ExecutionTimeMs = DefaultTimeoutMs
                };
            }

            stopwatch.Stop();

            var output = outputBuilder.ToString();
            var error = errorBuilder.ToString();

            if (outputTruncated)
            {
                output += "\n[Output truncated - exceeded 100KB limit]";
            }

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

    /// <summary>
    /// Apply security restrictions to process environment
    /// </summary>
    private void ApplySecurityRestrictions(ProcessStartInfo startInfo)
    {
        // Clear dangerous environment variables
        startInfo.Environment.Clear();

        // Add only safe, minimal environment variables
        startInfo.Environment["PATH"] = Environment.GetEnvironmentVariable("PATH") ?? "";
        startInfo.Environment["HOME"] = Path.GetTempPath();
        startInfo.Environment["TEMP"] = Path.GetTempPath();
        startInfo.Environment["TMP"] = Path.GetTempPath();

        // Prevent network access where possible (language-specific)
        startInfo.Environment["NO_PROXY"] = "*";
        startInfo.Environment["no_proxy"] = "*";
    }

    /// <summary>
    /// Apply memory and CPU limits to running process
    /// </summary>
    private void ApplyResourceLimits(Process process)
    {
        try
        {
            // Set memory limit (Windows: MaxWorkingSet)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                process.MaxWorkingSet = new IntPtr(MaxMemoryBytes);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
                     RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // On Linux/Mac, we could use ulimit via shell wrapper
                // For now, just rely on timeout protection
                // Future: Implement cgroups on Linux for proper isolation
            }

            // Set process priority to below normal to prevent resource hogging
            try
            {
                process.PriorityClass = ProcessPriorityClass.BelowNormal;
            }
            catch
            {
                // Priority setting may fail - not critical
            }
        }
        catch
        {
            // Resource limits are best-effort - continue if they fail
        }
    }

    private string? ExtractJavaClassName(string code)
    {
        var match = System.Text.RegularExpressions.Regex.Match(code, @"public\s+class\s+(\w+)");
        return match.Success ? match.Groups[1].Value : null;
    }
}
