using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CodeTutor.Native.Services;

public interface IRuntimeService
{
    Task<Dictionary<string, bool>> CheckRuntimesAsync();
    bool IsRuntimeAvailable(string language);
}

public class RuntimeService : IRuntimeService
{
    private readonly Dictionary<string, bool> _availability = new();
    private bool _checked = false;

    public bool IsRuntimeAvailable(string language)
    {
        if (!_checked) return true; // Assume yes until checked? Or wait? 
        // Ideally we check on startup.
        return _availability.GetValueOrDefault(language.ToLower(), false);
    }

    public async Task<Dictionary<string, bool>> CheckRuntimesAsync()
    {
        // Check Python (try both python and python3)
        _availability["python"] = await CheckCommandAsync("python", "--version") || await CheckCommandAsync("python3", "--version");

        // Check JavaScript/Node
        _availability["javascript"] = await CheckCommandAsync("node", "--version");

        // Check Java (java -version writes to stderr but exits 0 on success)
        _availability["java"] = await CheckCommandAsync("java", "-version");

        // Check Rust
        _availability["rust"] = await CheckCommandAsync("rustc", "--version");

        // Check C#/.NET
        _availability["csharp"] = await CheckCommandAsync("dotnet", "--version");

        // Check Dart/Flutter
        var dartAvailable = await CheckCommandAsync("dart", "--version");
        _availability["dart"] = dartAvailable;
        _availability["flutter"] = dartAvailable || await CheckCommandAsync("flutter", "--version");

        // Check Kotlin
        _availability["kotlin"] = await CheckCommandAsync("kotlinc", "-version");

        _checked = true;
        return _availability;
    }

    private async Task<bool> CheckCommandAsync(string command, string args)
    {
        try
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(startInfo);
            if (process == null) return false;

            await process.WaitForExitAsync();
            return process.ExitCode == 0;
        }
        catch
        {
            return false;
        }
    }
}
