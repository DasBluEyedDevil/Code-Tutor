using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace CodeTutor.Wpf.Services;

public class ModelDownloadProgress
{
    public string CurrentFile { get; set; } = string.Empty;
    public int FileIndex { get; set; }
    public int TotalFiles { get; set; }
    public long BytesDownloaded { get; set; }
    public long TotalBytes { get; set; }
    public double OverallProgress { get; set; }
}

public interface IModelDownloadService
{
    event EventHandler<ModelDownloadProgress>? ProgressChanged;
    event EventHandler<string>? StatusChanged;
    Task<bool> DownloadModelAsync(string targetDirectory, CancellationToken cancellationToken = default);
    Task<bool> IsModelDownloadedAsync(string modelDirectory);
}

public class ModelDownloadService : IModelDownloadService
{
    private readonly HttpClient _httpClient;
    private const string HuggingFaceBaseUrl = "https://huggingface.co";
    private const string ModelRepo = "microsoft/Phi-4-mini-instruct-onnx";
    private const string ModelPath = "gpu/gpu-int4-rtn-block-32";

    // Files required for the model to work
    private static readonly string[] RequiredFiles = new[]
    {
        "model.onnx",
        "model.onnx.data",
        "genai_config.json",
        "tokenizer.json",
        "tokenizer_config.json",
        "special_tokens_map.json",
        "added_tokens.json"
    };

    public event EventHandler<ModelDownloadProgress>? ProgressChanged;
    public event EventHandler<string>? StatusChanged;

    public ModelDownloadService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "CodeTutor/1.0");
    }

    public Task<bool> IsModelDownloadedAsync(string modelDirectory)
    {
        if (!Directory.Exists(modelDirectory))
            return Task.FromResult(false);

        // Check for the main model file
        var modelFile = Path.Combine(modelDirectory, "model.onnx");
        var configFile = Path.Combine(modelDirectory, "genai_config.json");

        return Task.FromResult(File.Exists(modelFile) && File.Exists(configFile));
    }

    public async Task<bool> DownloadModelAsync(string targetDirectory, CancellationToken cancellationToken = default)
    {
        try
        {
            // Create target directory
            Directory.CreateDirectory(targetDirectory);

            StatusChanged?.Invoke(this, "Fetching model file list...");

            // Get list of files to download
            var files = await GetFileListAsync(cancellationToken);
            if (files.Count == 0)
            {
                // Fallback to known required files if API doesn't work
                files = RequiredFiles.ToList();
            }

            long totalSize = 0;
            var fileSizes = new Dictionary<string, long>();

            // Get file sizes first
            StatusChanged?.Invoke(this, "Calculating download size...");
            foreach (var file in files)
            {
                var size = await GetFileSizeAsync(file, cancellationToken);
                fileSizes[file] = size;
                totalSize += size;
            }

            // Download each file
            long downloadedTotal = 0;
            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                var targetPath = Path.Combine(targetDirectory, file);

                // Create subdirectory if needed
                var dir = Path.GetDirectoryName(targetPath);
                if (!string.IsNullOrEmpty(dir))
                    Directory.CreateDirectory(dir);

                StatusChanged?.Invoke(this, $"Downloading {file}...");

                await DownloadFileAsync(
                    file,
                    targetPath,
                    (bytesDownloaded, fileTotal) =>
                    {
                        var progress = new ModelDownloadProgress
                        {
                            CurrentFile = file,
                            FileIndex = i + 1,
                            TotalFiles = files.Count,
                            BytesDownloaded = downloadedTotal + bytesDownloaded,
                            TotalBytes = totalSize,
                            OverallProgress = totalSize > 0
                                ? (double)(downloadedTotal + bytesDownloaded) / totalSize * 100
                                : 0
                        };
                        ProgressChanged?.Invoke(this, progress);
                    },
                    cancellationToken);

                downloadedTotal += fileSizes[file];
            }

            StatusChanged?.Invoke(this, "Download complete!");
            return true;
        }
        catch (OperationCanceledException)
        {
            StatusChanged?.Invoke(this, "Download cancelled.");
            return false;
        }
        catch (Exception ex)
        {
            StatusChanged?.Invoke(this, $"Download failed: {ex.Message}");
            return false;
        }
    }

    private async Task<List<string>> GetFileListAsync(CancellationToken cancellationToken)
    {
        try
        {
            // Use Hugging Face API to list files
            var apiUrl = $"{HuggingFaceBaseUrl}/api/models/{ModelRepo}/tree/main/{ModelPath}";
            var response = await _httpClient.GetStringAsync(apiUrl, cancellationToken);

            var files = JsonSerializer.Deserialize<List<HuggingFaceFileInfo>>(response);
            return files?
                .Where(f => f.Type == "file")
                .Select(f => f.Path?.Replace($"{ModelPath}/", "") ?? f.Path ?? "")
                .Where(f => !string.IsNullOrEmpty(f))
                .ToList() ?? new List<string>();
        }
        catch
        {
            // Return known required files as fallback
            return RequiredFiles.ToList();
        }
    }

    private async Task<long> GetFileSizeAsync(string fileName, CancellationToken cancellationToken)
    {
        try
        {
            var url = $"{HuggingFaceBaseUrl}/{ModelRepo}/resolve/main/{ModelPath}/{fileName}";
            using var request = new HttpRequestMessage(HttpMethod.Head, url);
            using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            return response.Content.Headers.ContentLength ?? 0;
        }
        catch
        {
            return 0;
        }
    }

    private async Task DownloadFileAsync(
        string fileName,
        string targetPath,
        Action<long, long> progressCallback,
        CancellationToken cancellationToken)
    {
        var url = $"{HuggingFaceBaseUrl}/{ModelRepo}/resolve/main/{ModelPath}/{fileName}";

        using var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        response.EnsureSuccessStatusCode();

        var totalBytes = response.Content.Headers.ContentLength ?? 0;

        await using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        await using var fileStream = new FileStream(targetPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);

        var buffer = new byte[81920]; // 80KB buffer
        long totalBytesRead = 0;
        int bytesRead;

        while ((bytesRead = await contentStream.ReadAsync(buffer, cancellationToken)) > 0)
        {
            await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
            totalBytesRead += bytesRead;
            progressCallback(totalBytesRead, totalBytes);
        }
    }

    private class HuggingFaceFileInfo
    {
        public string? Type { get; set; }
        public string? Path { get; set; }
        public long Size { get; set; }
    }
}
