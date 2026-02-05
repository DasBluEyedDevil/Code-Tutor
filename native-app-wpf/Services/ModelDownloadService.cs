using System.IO;
using System.Net.Http;

namespace CodeTutor.Wpf.Services;

public class ModelInfo
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long SizeBytes { get; set; }
    public string SizeFormatted => FormatBytes(SizeBytes);
    public string HuggingFaceRepo { get; set; } = string.Empty;
    public string ModelPath { get; set; } = string.Empty;
    public string[] RequiredFiles { get; set; } = Array.Empty<string>();
    public string LocalPath => Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "models", Id);

    private static string FormatBytes(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        int order = 0;
        double size = bytes;
        while (size >= 1024 && order < sizes.Length - 1)
        {
            order++;
            size /= 1024;
        }
        return $"{size:0.##} {sizes[order]}";
    }
}

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
    
    /// <summary>
    /// Get current model info (Qwen2.5-Coder-7B)
    /// </summary>
    ModelInfo GetCurrentModelInfo();
    
    /// <summary>
    /// Download and install the AI tutor model
    /// </summary>
    Task<bool> DownloadModelAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Uninstall and delete the AI tutor model to free disk space
    /// </summary>
    Task<bool> UninstallModelAsync();
    
    /// <summary>
    /// Check if the model is installed
    /// </summary>
    Task<bool> IsModelInstalledAsync();
    
    /// <summary>
    /// Get the disk space used by the model
    /// </summary>
    Task<long> GetModelSizeAsync();
}

public class ModelDownloadService : IModelDownloadService
{
    private readonly HttpClient _httpClient;
    private const string HuggingFaceBaseUrl = "https://huggingface.co";
    
    // Single model: Qwen2.5-Coder-7B
    private static readonly ModelInfo CurrentModel = new()
    {
        Id = "qwen2.5-coder-7b",
        Name = "Qwen2.5-Coder-7B",
        Description = "AI tutor model optimized for coding education. 2x faster than previous generation.",
        SizeBytes = 4_500_000_000, // ~4.5GB
        HuggingFaceRepo = "Qwen/Qwen2.5-Coder-7B-Instruct",
        ModelPath = "onnx/gpu-int4-rtn-block-32",
        RequiredFiles = new[]
        {
            "model.onnx",
            "model.onnx.data",
            "genai_config.json",
            "tokenizer.json",
            "tokenizer_config.json"
        }
    };

    public event EventHandler<ModelDownloadProgress>? ProgressChanged;
    public event EventHandler<string>? StatusChanged;

    public ModelDownloadService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "CodeTutor/1.0");
    }

    public ModelInfo GetCurrentModelInfo() => CurrentModel;

    public async Task<bool> IsModelInstalledAsync()
    {
        var modelDir = CurrentModel.LocalPath;
        if (!Directory.Exists(modelDir))
            return false;

        foreach (var file in CurrentModel.RequiredFiles)
        {
            var filePath = Path.Combine(modelDir, file);
            if (!File.Exists(filePath))
                return false;
        }

        return true;
    }

    public async Task<long> GetModelSizeAsync()
    {
        var modelDir = CurrentModel.LocalPath;
        if (!Directory.Exists(modelDir))
            return 0;

        var dirInfo = new DirectoryInfo(modelDir);
        return dirInfo.GetFiles("*", SearchOption.AllDirectories).Sum(f => f.Length);
    }

    public async Task<bool> DownloadModelAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var targetDirectory = CurrentModel.LocalPath;
            Directory.CreateDirectory(targetDirectory);

            StatusChanged?.Invoke(this, $"Downloading {CurrentModel.Name}...");

            var files = CurrentModel.RequiredFiles;
            long totalSize = CurrentModel.SizeBytes;
            long downloadedTotal = 0;

            for (int i = 0; i < files.Length; i++)
            {
                var file = files[i];
                var targetPath = Path.Combine(targetDirectory, file);

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
                            TotalFiles = files.Length,
                            BytesDownloaded = downloadedTotal + bytesDownloaded,
                            TotalBytes = totalSize,
                            OverallProgress = totalSize > 0
                                ? (double)(downloadedTotal + bytesDownloaded) / totalSize * 100
                                : 0
                        };
                        ProgressChanged?.Invoke(this, progress);
                    },
                    cancellationToken);

                var fileInfo = new FileInfo(targetPath);
                downloadedTotal += fileInfo.Length;
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

    public async Task<bool> UninstallModelAsync()
    {
        try
        {
            var modelDir = CurrentModel.LocalPath;
            if (!Directory.Exists(modelDir))
            {
                StatusChanged?.Invoke(this, "Model not found.");
                return true;
            }

            StatusChanged?.Invoke(this, $"Uninstalling {CurrentModel.Name}...");

            Directory.Delete(modelDir, true);

            StatusChanged?.Invoke(this, "Model uninstalled. Disk space freed.");
            return true;
        }
        catch (Exception ex)
        {
            StatusChanged?.Invoke(this, $"Uninstall failed: {ex.Message}");
            return false;
        }
    }

    private async Task DownloadFileAsync(
        string fileName,
        string targetPath,
        Action<long, long> progressCallback,
        CancellationToken cancellationToken)
    {
        var url = $"{HuggingFaceBaseUrl}/{CurrentModel.HuggingFaceRepo}/resolve/main/{CurrentModel.ModelPath}/{fileName}";

        using var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        response.EnsureSuccessStatusCode();

        var totalBytes = response.Content.Headers.ContentLength ?? 0;

        await using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        await using var fileStream = new FileStream(targetPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);

        var buffer = new byte[81920];
        long totalBytesRead = 0;
        int bytesRead;

        while ((bytesRead = await contentStream.ReadAsync(buffer, cancellationToken)) > 0)
        {
            await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
            totalBytesRead += bytesRead;
            progressCallback(totalBytesRead, totalBytes);
        }
    }
}
