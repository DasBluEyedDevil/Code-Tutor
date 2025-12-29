using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CodeTutor.Wpf.Services.Executors;

namespace CodeTutor.Wpf.Services;

public interface ICodeExecutionService
{
    Task<ExecutionResult> ExecuteAsync(string code, string language);
    Task<RuntimeInfo> GetRuntimeInfoAsync(string language);
    bool IsPistonAvailable { get; }
}

public record ExecutionResult(bool Success, string Output, string Error);

public class CodeExecutionService : ICodeExecutionService
{
    private readonly RoslynCSharpExecutor _roslynExecutor;
    private readonly PistonExecutor _pistonExecutor;
    private readonly RuntimeDetectionService _runtimeDetection;
    private bool? _pistonAvailable;

    public bool IsPistonAvailable => _pistonAvailable ?? false;

    public CodeExecutionService()
    {
        _roslynExecutor = new RoslynCSharpExecutor();
        _pistonExecutor = new PistonExecutor();
        _runtimeDetection = new RuntimeDetectionService();

        // Check Piston availability in background
        _ = CheckPistonAsync();
    }

    private async Task CheckPistonAsync()
    {
        _pistonAvailable = await _pistonExecutor.IsAvailableAsync();
    }

    public async Task<RuntimeInfo> GetRuntimeInfoAsync(string language)
    {
        return await _runtimeDetection.CheckRuntimeAsync(language);
    }

    public async Task<ExecutionResult> ExecuteAsync(string code, string language)
    {
        var langLower = language.ToLowerInvariant();

        // C# always uses Roslyn (no external dependency)
        if (langLower is "csharp" or "c#")
        {
            return await _roslynExecutor.ExecuteAsync(code);
        }

        // Try Piston first if available (sandboxed)
        if (_pistonAvailable == true)
        {
            var pistonResult = await _pistonExecutor.ExecuteAsync(code, language);
            if (!pistonResult.Error.Contains("connection failed"))
                return pistonResult;
        }

        // Fallback to local execution
        return langLower switch
        {
            "python" => await ExecuteLocalAsync("python", code, ".py"),
            "javascript" or "js" => await ExecuteLocalAsync("node", code, ".js"),
            "java" => await ExecuteJavaAsync(code),
            "kotlin" => await ExecuteLocalAsync("kotlinc", code, ".kts", "-script"),
            "rust" => await ExecuteRustAsync(code),
            "dart" or "flutter" => await ExecuteLocalAsync("dart", code, ".dart", "run"),
            _ => new ExecutionResult(false, "", $"Language '{language}' not supported")
        };
    }

    private async Task<ExecutionResult> ExecuteLocalAsync(string command, string code, string extension, string? extraArgs = null)
    {
        var runtimeInfo = await _runtimeDetection.CheckRuntimeAsync(command);
        if (!runtimeInfo.IsAvailable)
        {
            return new ExecutionResult(false, "",
                $"{command} is not installed.\n\n{runtimeInfo.InstallHint}");
        }

        var tempFile = Path.Combine(Path.GetTempPath(), $"codetutor_{Guid.NewGuid()}{extension}");
        await File.WriteAllTextAsync(tempFile, code);

        try
        {
            var args = extraArgs != null ? $"{extraArgs} \"{tempFile}\"" : $"\"{tempFile}\"";
            return await RunProcessAsync(command, args);
        }
        finally
        {
            try { File.Delete(tempFile); } catch { }
        }
    }

    private async Task<ExecutionResult> ExecuteJavaAsync(string code)
    {
        var runtimeInfo = await _runtimeDetection.CheckRuntimeAsync("java");
        if (!runtimeInfo.IsAvailable)
        {
            return new ExecutionResult(false, "",
                $"Java is not installed.\n\n{runtimeInfo.InstallHint}");
        }

        var className = "Main";
        var classMatch = Regex.Match(code, @"public\s+class\s+(\w+)");
        if (classMatch.Success)
            className = classMatch.Groups[1].Value;

        var tempDir = Path.Combine(Path.GetTempPath(), $"codetutor_{Guid.NewGuid()}");
        Directory.CreateDirectory(tempDir);

        try
        {
            var javaFile = Path.Combine(tempDir, $"{className}.java");
            await File.WriteAllTextAsync(javaFile, code);

            var compileResult = await RunProcessAsync("javac", $"\"{javaFile}\"");
            if (!compileResult.Success)
                return new ExecutionResult(false, "", compileResult.Error);

            return await RunProcessAsync("java", $"-cp \"{tempDir}\" {className}");
        }
        finally
        {
            try { Directory.Delete(tempDir, true); } catch { }
        }
    }

    private async Task<ExecutionResult> ExecuteRustAsync(string code)
    {
        var runtimeInfo = await _runtimeDetection.CheckRuntimeAsync("rust");
        if (!runtimeInfo.IsAvailable)
        {
            return new ExecutionResult(false, "",
                $"Rust is not installed.\n\n{runtimeInfo.InstallHint}");
        }

        var tempDir = Path.Combine(Path.GetTempPath(), $"codetutor_{Guid.NewGuid()}");
        Directory.CreateDirectory(tempDir);

        try
        {
            var rustFile = Path.Combine(tempDir, "main.rs");
            var exeFile = Path.Combine(tempDir, "main.exe");
            await File.WriteAllTextAsync(rustFile, code);

            var compileResult = await RunProcessAsync("rustc", $"\"{rustFile}\" -o \"{exeFile}\"");
            if (!compileResult.Success)
                return new ExecutionResult(false, "", compileResult.Error);

            return await RunProcessAsync(exeFile, "");
        }
        finally
        {
            try { Directory.Delete(tempDir, true); } catch { }
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

            var outputTask = process.StandardOutput.ReadToEndAsync();
            var errorTask = process.StandardError.ReadToEndAsync();

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
