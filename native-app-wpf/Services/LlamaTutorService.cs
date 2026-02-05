using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using CodeTutor.Wpf.Models;
using LLama;
using LLama.Common;

namespace CodeTutor.Wpf.Services;

/// <summary>
/// Tutor service implementation using LLamaSharp with GGUF models.
/// Supports Qwen2.5-Coder and other GGUF format models without ONNX conversion.
/// </summary>
public class LlamaTutorService : ITutorService, IDisposable
{
    private LLamaWeights? _model;
    private LLamaContext? _context;
    private readonly string _modelPath;
    private readonly string _systemPrompt;
    private bool _disposed;

    public bool IsModelLoaded => _model != null && _context != null;
    public int LoadingProgress { get; private set; }
    public event EventHandler<int>? LoadingProgressChanged;

    public LlamaTutorService()
    {
        // Get the directory where the assembly is located (works for both dotnet run and EXE)
        var assemblyLocation = Assembly.GetExecutingAssembly().Location;
        var assemblyDirectory = Path.GetDirectoryName(assemblyLocation) ?? AppDomain.CurrentDomain.BaseDirectory;
        
        // Model path for GGUF format
        _modelPath = Path.Combine(assemblyDirectory, "models", "qwen2.5-coder-7b", "model.gguf");
        
        _systemPrompt = "You are an expert coding tutor specializing in helping beginners learn programming. " +
            "Explain concepts clearly with code examples. Help debug errors step by step. " +
            "Be concise but thorough.";
    }

    public async Task LoadModelAsync(CancellationToken cancellationToken = default)
    {
        if (IsModelLoaded) return;

        await Task.Run(() =>
        {
            UpdateProgress(10);

            // Debug: Log the path being used
            System.Diagnostics.Debug.WriteLine($"[LlamaTutorService] Looking for model at: {_modelPath}");
            System.Diagnostics.Debug.WriteLine($"[LlamaTutorService] File exists: {File.Exists(_modelPath)}");

            if (!File.Exists(_modelPath))
            {
                // Try to find the model file in alternative locations
                var alternativePaths = new[]
                {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "models", "qwen2.5-coder-7b", "model.gguf"),
                    Path.Combine(Environment.CurrentDirectory, "models", "qwen2.5-coder-7b", "model.gguf"),
                    Path.Combine(Directory.GetCurrentDirectory(), "models", "qwen2.5-coder-7b", "model.gguf"),
                };

                foreach (var altPath in alternativePaths)
                {
                    System.Diagnostics.Debug.WriteLine($"[LlamaTutorService] Trying alternative: {altPath}");
                    if (File.Exists(altPath))
                    {
                        System.Diagnostics.Debug.WriteLine($"[LlamaTutorService] Found at alternative path!");
                        break;
                    }
                }

                throw new FileNotFoundException(
                    $"The AI Tutor model was not found at '{_modelPath}'. " +
                    "Please download it from the AI Tutor panel.");
            }

            // Check file size (should be ~4.5GB for Q4_K_M)
            var fileInfo = new FileInfo(_modelPath);
            System.Diagnostics.Debug.WriteLine($"[LlamaTutorService] Model file size: {fileInfo.Length / 1024 / 1024}MB");
            
            if (fileInfo.Length < 4_000_000_000) // Less than 4GB is likely incomplete
            {
                throw new InvalidOperationException(
                    $"Model file appears incomplete ({fileInfo.Length / 1024 / 1024}MB, expected ~4500MB). Please re-download.");
            }

            UpdateProgress(30);

            // Load model parameters
            var parameters = new ModelParams(_modelPath)
            {
                ContextSize = 2048,   // Reduced context window for faster inference
                GpuLayerCount = 0,    // CPU only for compatibility (set to 20+ for GPU)
                Threads = Environment.ProcessorCount / 2, // Use half the CPU cores
                BatchSize = 512,      // Smaller batch for lower memory
            };

            UpdateProgress(50);
            _model = LLamaWeights.LoadFromFile(parameters);

            UpdateProgress(70);
            _context = _model.CreateContext(parameters);

            UpdateProgress(100);
        }, cancellationToken);
    }

    public async Task WarmUpAsync(CancellationToken cancellationToken = default)
    {
        if (!IsModelLoaded || _context == null) return;

        // Simple warm-up inference
        await Task.Run(() =>
        {
            try
            {
                var executor = new InteractiveExecutor(_context);
                var session = new ChatSession(executor);
                session.AddSystemMessage(_systemPrompt);
                
                var warmupResult = session.ChatAsync(
                    new ChatHistory.Message(AuthorRole.User, "Hello"),
                    inferenceParams: new InferenceParams { MaxTokens = 1 });
                
                // Consume one token
                var enumerator = warmupResult.GetAsyncEnumerator();
                enumerator.MoveNextAsync().AsTask().Wait();
                enumerator.DisposeAsync().AsTask().Wait();
            }
            catch
            {
                // Warm-up failures are non-critical
            }
        }, cancellationToken);
    }

    public async IAsyncEnumerable<string> SendMessageAsync(
        string userMessage,
        TutorContext context,
        IReadOnlyList<TutorMessage> history,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (!IsModelLoaded || _context == null)
            throw new InvalidOperationException("Model not loaded. Call LoadModelAsync first.");

        // Build context-aware message
        var contextualMessage = BuildContextualMessage(userMessage, context);

        // Yield immediately to show "Thinking..."
        await Task.Yield();

        // Create a fresh session for each conversation to avoid history corruption
        var executor = new InteractiveExecutor(_context);
        var session = new ChatSession(executor);
        
        // Add system prompt
        session.AddSystemMessage(_systemPrompt);

        // Add conversation history - ensure strict alternation between user and assistant
        // Filter to valid alternating pairs only
        var validHistory = GetValidHistoryPairs(history);
        foreach (var (user, assistant) in validHistory)
        {
            session.AddUserMessage(user);
            if (!string.IsNullOrEmpty(assistant))
            {
                session.AddAssistantMessage(assistant);
            }
        }

        var inferenceParams = new InferenceParams
        {
            MaxTokens = 256,      // Reduced for faster response
            AntiPrompts = new[] { "<|im_end|>", "<|im_start|>", "User:", "Human:" },
        };

        // Generate response
        IAsyncEnumerable<string> result;
        
        result = session.ChatAsync(
            new ChatHistory.Message(AuthorRole.User, contextualMessage),
            inferenceParams: inferenceParams);

        await using var enumerator = result.GetAsyncEnumerator(cancellationToken);
        bool hasTokens = false;
        
        while (await enumerator.MoveNextAsync())
        {
            hasTokens = true;
            yield return enumerator.Current;
        }

        if (!hasTokens)
        {
            yield return "[Model returned empty response - check if model file is complete]";
        }
    }

    /// <summary>
    /// Extracts valid user-assistant pairs from history, ensuring strict alternation.
    /// </summary>
    private List<(string user, string assistant)> GetValidHistoryPairs(IReadOnlyList<TutorMessage> history)
    {
        var pairs = new List<(string user, string assistant)>();
        if (history == null || history.Count == 0) return pairs;

        string? pendingUser = null;
        
        foreach (var msg in history)
        {
            if (msg.Role == MessageRole.User)
            {
                // If we had a pending user message without an assistant response, skip it
                // and start fresh with this one
                pendingUser = msg.Content;
            }
            else if (msg.Role == MessageRole.Assistant && pendingUser != null)
            {
                // Complete pair
                pairs.Add((pendingUser, msg.Content));
                pendingUser = null;
            }
        }

        return pairs;
    }

    private string BuildContextualMessage(string userMessage, TutorContext context)
    {
        var sb = new StringBuilder();

        if (!string.IsNullOrEmpty(context.CurrentLanguage))
            sb.AppendLine($"[Language: {context.CurrentLanguage}]");
        if (!string.IsNullOrEmpty(context.LessonTitle))
            sb.AppendLine($"[Lesson: {context.LessonTitle}]");
        if (!string.IsNullOrEmpty(context.UserCode))
        {
            var codeLines = context.UserCode.Split('\n').Take(15);
            sb.AppendLine($"[Code:]\n```{context.CurrentLanguage?.ToLower() ?? ""}\n{string.Join("\n", codeLines)}\n```");
        }
        if (!string.IsNullOrEmpty(context.ExecutionError))
            sb.AppendLine($"[Error: {context.ExecutionError}]");

        sb.AppendLine(userMessage);

        return sb.ToString();
    }

    private void UpdateProgress(int progress)
    {
        LoadingProgress = progress;
        LoadingProgressChanged?.Invoke(this, progress);
    }

    public void UnloadModel()
    {
        _context?.Dispose();
        _context = null;
        _model?.Dispose();
        _model = null;
        LoadingProgress = 0;
    }

    public void Dispose()
    {
        if (_disposed) return;
        UnloadModel();
        _disposed = true;
    }
}
