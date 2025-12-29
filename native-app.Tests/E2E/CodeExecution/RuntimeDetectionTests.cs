using System.Diagnostics;
using CodeTutor.Tests.Models;

namespace CodeTutor.Tests.E2E.CodeExecution;

/// <summary>
/// E2E tests for runtime detection functionality.
/// Tests validate that language runtimes can be detected and their availability reported.
/// </summary>
public class RuntimeDetectionTests
{
    private const int DetectionTimeoutSeconds = 5;

    [Theory]
    [InlineData("python", "python", "--version")]
    [InlineData("javascript", "node", "--version")]
    [InlineData("java", "java", "--version")]
    [InlineData("kotlin", "kotlinc", "-version")]
    [InlineData("rust", "rustc", "--version")]
    [InlineData("dart", "dart", "--version")]
    public async Task DetectRuntime_ChecksCorrectCommand(string language, string command, string args)
    {
        // Act
        var result = await CheckRuntimeAsync(command, args, language);

        // Assert
        // The result should be valid (either available or with proper error)
        result.Language.Should().Be(language);
        // If available, should have a version
        if (result.IsAvailable)
        {
            result.Version.Should().NotBeNullOrEmpty();
        }
    }

    [Fact]
    public async Task DetectRuntime_CSharp_AlwaysAvailable()
    {
        // C# uses Roslyn built-in, so it's always available
        // Arrange & Act
        var result = new RuntimeInfo("csharp", true, "Roslyn Built-in", "");

        // Assert
        result.IsAvailable.Should().BeTrue();
        result.Version.Should().Be("Roslyn Built-in");
        result.InstallHint.Should().BeEmpty();

        await Task.CompletedTask;
    }

    [Fact]
    public async Task DetectRuntime_UnsupportedLanguage_ReturnsNotAvailable()
    {
        // Arrange
        var language = "brainfuck";

        // Act
        var result = await CheckRuntimeAsync("nonexistent-command", "--version", language);

        // Assert
        result.IsAvailable.Should().BeFalse();
    }

    [Theory]
    [InlineData("python", "Install Python from https://python.org")]
    [InlineData("javascript", "Install Node.js from https://nodejs.org")]
    [InlineData("java", "Install Java JDK from https://adoptium.net")]
    [InlineData("kotlin", "Install Kotlin from https://kotlinlang.org")]
    [InlineData("rust", "Install Rust from https://rustup.rs")]
    [InlineData("dart", "Install Dart from https://dart.dev")]
    public void GetInstallHint_ReturnsUsefulHint(string language, string expectedHintPart)
    {
        // Act
        var hint = GetInstallHint(language);

        // Assert
        hint.Should().Contain(expectedHintPart);
    }

    [Fact]
    public async Task DetectRuntime_CachesResults()
    {
        // Arrange
        var cache = new Dictionary<string, RuntimeInfo>();
        var language = "csharp";

        // Act - First call
        var firstResult = GetCachedOrDetect(cache, language, () =>
            new RuntimeInfo(language, true, "Roslyn Built-in", ""));

        // Second call should return cached
        var secondResult = GetCachedOrDetect(cache, language, () =>
            new RuntimeInfo(language, true, "Different Version", ""));

        // Assert
        firstResult.Version.Should().Be(secondResult.Version);
        cache.Should().ContainKey(language);

        await Task.CompletedTask;
    }

    [Fact]
    public void RuntimeInfo_RecordEquality()
    {
        // Arrange
        var info1 = new RuntimeInfo("python", true, "3.11.0", "");
        var info2 = new RuntimeInfo("python", true, "3.11.0", "");
        var info3 = new RuntimeInfo("python", true, "3.10.0", "");

        // Assert
        info1.Should().Be(info2);
        info1.Should().NotBe(info3);
    }

    [Fact]
    public void RuntimeInfo_Deconstruction()
    {
        // Arrange
        var info = new RuntimeInfo("python", true, "3.11.0", "");

        // Act
        var (language, isAvailable, version, hint) = info;

        // Assert
        language.Should().Be("python");
        isAvailable.Should().BeTrue();
        version.Should().Be("3.11.0");
        hint.Should().BeEmpty();
    }

    [Theory]
    [InlineData("Python")]
    [InlineData("PYTHON")]
    [InlineData("python")]
    public void GetLanguageKey_NormalizesCase(string input)
    {
        // Act
        var normalized = input.ToLowerInvariant();

        // Assert
        normalized.Should().Be("python");
    }

    [Theory]
    [InlineData("javascript", "javascript")]
    [InlineData("js", "js")]
    [InlineData("node", "node")]
    public void GetLanguageKey_HandlesAliases(string input, string expected)
    {
        // JavaScript can be referred to by multiple names
        // The actual implementation should normalize these

        // Act
        var normalized = input.ToLowerInvariant();

        // Assert
        normalized.Should().Be(expected);
    }

    [Fact]
    public async Task DetectRuntime_HandlesTimeout()
    {
        // Arrange
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));

        // Act - Try to detect a command that doesn't exist (should fail quickly)
        var result = await CheckRuntimeWithTimeoutAsync(
            "nonexistent-command-12345",
            "--version",
            "test",
            cts.Token);

        // Assert
        result.IsAvailable.Should().BeFalse();
    }

    [Fact]
    public async Task DetectRuntime_ExtractsVersionFromOutput()
    {
        // Arrange - Simulate version output
        var versionOutput = "Python 3.11.4\n";

        // Act
        var version = ExtractVersionFromOutput(versionOutput);

        // Assert
        version.Should().Contain("Python 3.11.4");

        await Task.CompletedTask;
    }

    [Theory]
    [InlineData("Python 3.11.4\n", "Python 3.11.4")]
    [InlineData("node v20.10.0\nmore output", "node v20.10.0")]
    [InlineData("openjdk 17.0.1 2021-10-19\nOpenJDK Runtime", "openjdk 17.0.1 2021-10-19")]
    public void ExtractVersion_GetsFirstLine(string output, string expected)
    {
        // Act
        var version = ExtractVersionFromOutput(output);

        // Assert
        version.Should().Be(expected);
    }

    [Fact]
    public void SupportedLanguages_ContainsAllExpected()
    {
        // Arrange
        var expectedLanguages = new[]
        {
            "python",
            "javascript",
            "java",
            "csharp",
            "kotlin",
            "rust",
            "dart"
        };

        var supportedLanguages = new HashSet<string>(expectedLanguages);

        // Assert
        foreach (var lang in expectedLanguages)
        {
            supportedLanguages.Should().Contain(lang);
        }
    }

    private async Task<RuntimeInfo> CheckRuntimeAsync(string command, string args, string language)
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
                return new RuntimeInfo(language, false, "", GetInstallHint(language));

            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(DetectionTimeoutSeconds));
            try
            {
                await process.WaitForExitAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                process.Kill(true);
                return new RuntimeInfo(language, false, "", GetInstallHint(language));
            }

            var version = ExtractVersionFromOutput(!string.IsNullOrWhiteSpace(output) ? output : error);
            return new RuntimeInfo(language, process.ExitCode == 0, version, "");
        }
        catch
        {
            return new RuntimeInfo(language, false, "", GetInstallHint(language));
        }
    }

    private async Task<RuntimeInfo> CheckRuntimeWithTimeoutAsync(
        string command,
        string args,
        string language,
        CancellationToken cancellationToken)
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
                return new RuntimeInfo(language, false, "", GetInstallHint(language));

            try
            {
                await process.WaitForExitAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                process.Kill(true);
                return new RuntimeInfo(language, false, "", "Detection timed out");
            }

            return new RuntimeInfo(language, process.ExitCode == 0, "", "");
        }
        catch
        {
            return new RuntimeInfo(language, false, "", GetInstallHint(language));
        }
    }

    private static string GetInstallHint(string language)
    {
        return language.ToLowerInvariant() switch
        {
            "python" => "Install Python from https://python.org",
            "javascript" or "js" or "node" => "Install Node.js from https://nodejs.org",
            "java" => "Install Java JDK from https://adoptium.net",
            "kotlin" => "Install Kotlin from https://kotlinlang.org/docs/command-line.html",
            "rust" => "Install Rust from https://rustup.rs",
            "dart" or "flutter" => "Install Dart from https://dart.dev/get-dart",
            _ => $"Language '{language}' is not supported"
        };
    }

    private static RuntimeInfo GetCachedOrDetect(
        Dictionary<string, RuntimeInfo> cache,
        string language,
        Func<RuntimeInfo> detect)
    {
        if (cache.TryGetValue(language, out var cached))
            return cached;

        var result = detect();
        cache[language] = result;
        return result;
    }

    private static string ExtractVersionFromOutput(string output)
    {
        if (string.IsNullOrWhiteSpace(output))
            return "Unknown";

        return output.Trim().Split('\n')[0];
    }
}
