using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace CodeTutor.Wpf.Services.Executors;

public class PistonExecutor
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public PistonExecutor(string baseUrl = "http://localhost:2000")
    {
        _baseUrl = baseUrl.TrimEnd('/');
        _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(35) };
    }

    public async Task<bool> IsAvailableAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/v2/runtimes");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<ExecutionResult> ExecuteAsync(string code, string language, CancellationToken cancellationToken = default)
    {
        var pistonLang = MapLanguage(language);
        if (pistonLang == null)
            return new ExecutionResult(false, "", $"Language '{language}' not supported by Piston");

        var request = new PistonRequest
        {
            Language = pistonLang.Language,
            Version = pistonLang.Version,
            Files = new[] { new PistonFile { Content = code } }
        };

        try
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/api/v2/execute", content, cancellationToken);
            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
                return new ExecutionResult(false, "", $"Piston error: {response.StatusCode}");

            var result = JsonSerializer.Deserialize<PistonResponse>(responseBody);
            if (result == null)
                return new ExecutionResult(false, "", "Invalid response from Piston");

            var output = result.Run?.Output?.Trim() ?? "";
            var stderr = result.Run?.Stderr?.Trim() ?? "";
            var exitCode = result.Run?.Code ?? 0;

            return new ExecutionResult(
                exitCode == 0 && string.IsNullOrEmpty(stderr),
                output,
                stderr
            );
        }
        catch (OperationCanceledException)
        {
            return new ExecutionResult(false, "", "Execution timed out");
        }
        catch (Exception ex)
        {
            return new ExecutionResult(false, "", $"Piston connection failed: {ex.Message}");
        }
    }

    private static PistonLanguageInfo? MapLanguage(string language) => language.ToLowerInvariant() switch
    {
        "python" => new("python", "3.10.0"),
        "javascript" or "js" => new("javascript", "18.15.0"),
        "csharp" or "c#" => new("csharp", "6.12.0"),
        "java" => new("java", "15.0.2"),
        "kotlin" => new("kotlin", "1.8.20"),
        "rust" => new("rust", "1.68.2"),
        "dart" or "flutter" => new("dart", "2.19.6"),
        _ => null
    };

    private record PistonLanguageInfo(string Language, string Version);
}

file class PistonRequest
{
    [JsonPropertyName("language")]
    public string Language { get; set; } = "";

    [JsonPropertyName("version")]
    public string Version { get; set; } = "";

    [JsonPropertyName("files")]
    public PistonFile[] Files { get; set; } = Array.Empty<PistonFile>();
}

file class PistonFile
{
    [JsonPropertyName("content")]
    public string Content { get; set; } = "";
}

file class PistonResponse
{
    [JsonPropertyName("run")]
    public PistonRun? Run { get; set; }
}

file class PistonRun
{
    [JsonPropertyName("stdout")]
    public string? Output { get; set; }

    [JsonPropertyName("stderr")]
    public string? Stderr { get; set; }

    [JsonPropertyName("code")]
    public int Code { get; set; }
}
