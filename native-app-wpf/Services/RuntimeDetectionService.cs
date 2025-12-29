using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CodeTutor.Wpf.Services;

public record RuntimeInfo(string Language, bool IsAvailable, string Version, string InstallHint);

public class RuntimeDetectionService
{
    private readonly ConcurrentDictionary<string, RuntimeInfo> _cache = new();

    public async Task<RuntimeInfo> CheckRuntimeAsync(string language)
    {
        var langKey = language.ToLowerInvariant();

        if (_cache.TryGetValue(langKey, out var cached))
            return cached;

        var info = langKey switch
        {
            "python" => await CheckCommandAsync("python", "--version", "python",
                "Install Python from https://python.org"),
            "javascript" or "js" => await CheckCommandAsync("node", "--version", "javascript",
                "Install Node.js from https://nodejs.org"),
            "java" => await CheckCommandAsync("java", "--version", "java",
                "Install Java JDK from https://adoptium.net"),
            "kotlin" => await CheckCommandAsync("kotlinc", "-version", "kotlin",
                "Install Kotlin from https://kotlinlang.org/docs/command-line.html"),
            "rust" => await CheckCommandAsync("rustc", "--version", "rust",
                "Install Rust from https://rustup.rs"),
            "dart" or "flutter" => await CheckCommandAsync("dart", "--version", "dart",
                "Install Dart from https://dart.dev/get-dart"),
            "csharp" or "c#" => new RuntimeInfo("csharp", true, "Roslyn Built-in", ""),
            _ => new RuntimeInfo(langKey, false, "", $"Language '{language}' is not supported")
        };

        _cache[langKey] = info;
        return info;
    }

    private async Task<RuntimeInfo> CheckCommandAsync(string command, string args, string language, string installHint)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = command,
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            if (process == null)
                return new RuntimeInfo(language, false, "", installHint);

            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            try
            {
                await process.WaitForExitAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                process.Kill(true);
                return new RuntimeInfo(language, false, "", installHint);
            }

            var version = !string.IsNullOrWhiteSpace(output) ? output.Trim().Split('\n')[0] :
                          !string.IsNullOrWhiteSpace(error) ? error.Trim().Split('\n')[0] : "Unknown";

            return new RuntimeInfo(language, process.ExitCode == 0, version, "");
        }
        catch (OperationCanceledException)
        {
            return new RuntimeInfo(language, false, "", installHint);
        }
        catch (Exception)
        {
            return new RuntimeInfo(language, false, "", installHint);
        }
    }

    public void ClearCache() => _cache.Clear();
}
